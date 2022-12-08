--------------------------------------------------------
--  ファイルを作成しました - 水曜日-3月-10-2021   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Procedure SCAN_BATCH_CTL_EXTRACTION
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "DBCTR"."SCAN_BATCH_CTL_EXTRACTION" (
  i_MODE IN NUMBER
 ,i_INPUT_ROUTE IN NUMBER
 ,i_SCAN_DATE IN NUMBER
 ,i_LOCK IN VARCHAR2
 ,i_BATCH_FOLDER_NAME IN VARCHAR2
 ,o_INPUT_ROUTE OUT NUMBER
 ,o_BATCH_FOLDER_NAME OUT VARCHAR2
 ,o_RESULT OUT NUMBER
 ,o_ERRCODE OUT NUMBER
 ,o_ERRMSG OUT VARCHAR2
)
IS
 l_FOLDER_NAME VARCHAR2(20) := NULL;
 l_STATUS NUMBER(1, 0) := -1;
BEGIN

    IF i_MODE = 1 THEN
        /* 自動選択モード */
        FOR i IN 1..10 LOOP

            BEGIN
                /* 指定があれば対象のBATCH_FOLDER以上のデータを対象とする(NULLの場合は指定がなしのため全部)  */
                SELECT BATCH_FOLDER_NAME into l_FOLDER_NAME
                FROM(
                  SELECT BATCH_FOLDER_NAME
                  FROM DBCTR.SCAN_BATCH_CTL
                  WHERE INPUT_ROUTE = i_INPUT_ROUTE
                    AND ( SCAN_DATE = 0 OR SCAN_DATE = i_SCAN_DATE )
                    AND ( i_BATCH_FOLDER_NAME IS NULL OR BATCH_FOLDER_NAME >= i_BATCH_FOLDER_NAME )
                    AND STATUS = 1
                  ORDER BY BATCH_FOLDER_NAME
                )
                WHERE ROWNUM =1
                ;
            EXCEPTION
                WHEN NO_DATA_FOUND THEN
                    o_RESULT := 1;
                    RETURN;
            END;

            BEGIN
                SELECT BATCH_FOLDER_NAME into l_FOLDER_NAME
                FROM DBCTR.SCAN_BATCH_CTL
                WHERE INPUT_ROUTE = i_INPUT_ROUTE
                  AND BATCH_FOLDER_NAME = l_FOLDER_NAME
                  AND STATUS = 1
                FOR UPDATE NOWAIT
                ;
            EXCEPTION
                WHEN OTHERS THEN
                    l_FOLDER_NAME := NULL;
                    CONTINUE;
            END;

            IF l_FOLDER_NAME IS NOT NULL THEN
                EXIT;
            END IF;

        END LOOP;

        IF l_FOLDER_NAME IS NULL THEN
            RAISE_APPLICATION_ERROR(-20001, 'LOOP Error');
        END IF;

        UPDATE DBCTR.SCAN_BATCH_CTL
         SET STATUS = 3,
             LOCK_TERM = i_LOCK
        WHERE INPUT_ROUTE = i_INPUT_ROUTE
          AND BATCH_FOLDER_NAME = l_FOLDER_NAME
        ;

        o_INPUT_ROUTE := i_INPUT_ROUTE;
        o_BATCH_FOLDER_NAME := l_FOLDER_NAME;
        o_RESULT := 0;

    ELSIF i_MODE = 2 THEN
        o_INPUT_ROUTE := 0;

        BEGIN
            SELECT BATCH_FOLDER_NAME, STATUS into l_FOLDER_NAME,l_STATUS
            FROM DBCTR.SCAN_BATCH_CTL
            WHERE INPUT_ROUTE = i_INPUT_ROUTE
              AND BATCH_FOLDER_NAME=i_BATCH_FOLDER_NAME
              AND STATUS  IN(1, 2, 9)
            FOR UPDATE NOWAIT
            ;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                o_RESULT := 1;
                RETURN;
        END;

        IF l_STATUS IN(1, 2) THEN

            UPDATE DBCTR.SCAN_BATCH_CTL
             SET STATUS = 3,
                 LOCK_TERM = i_LOCK
            WHERE INPUT_ROUTE = i_INPUT_ROUTE
              AND BATCH_FOLDER_NAME = l_FOLDER_NAME
            ;
        END IF;

        o_INPUT_ROUTE := i_INPUT_ROUTE;
        o_BATCH_FOLDER_NAME := l_FOLDER_NAME;
        o_RESULT := 0;
    END IF;

    COMMIT;

EXCEPTION
  WHEN OTHERS THEN
      o_ERRCODE := SQLCODE;
      o_ERRMSG := SQLERRM;
      o_RESULT := 2;
      ROLLBACK;

END;

/
exit;