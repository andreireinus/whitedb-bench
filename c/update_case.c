#include <whitedb/dbapi.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

void *create_database() {
	void *db;
    void *record;
    char *name = "1";
    int i, j;
    int field_count = 100;

    db = wg_attach_database(name, 1073741824);  // 1 gigabyte
    if (!db) {
        printf("failed at create database\n");
        exit(0);
    }
    for (i = 0; i < 680000; i++) {
        record = wg_create_record(db, field_count);
        for (j = 0; j < field_count; j++) {
            wg_set_int_field(db, record, j, j);
        }
    }

	return db;
}

int run_test(void *db, int nr) {
    void *record;
    int i, j;
    int field_count = 100;

    record = wg_get_first_record(db);
    while (record != NULL) {
        for (j = 0; j < field_count; j++)
        {
            wg_set_int_field(db, record, j, j + 1);
        }
        record = wg_get_next_record(db, record);
    }

    return 0;
}



int main(int argc, char **argv) {
    void *db;
    struct timespec start, stop;
    int i;

    db = create_database();
    for (i = 0; i < run_count(); i++) {
        clock_gettime(CLOCK_REALTIME, &start);
        run_test(db, i);
        clock_gettime(CLOCK_REALTIME, &stop);
        diff(start, stop);
    }

    wg_detach_database(db);
    wg_delete_database("1");

    return (EXIT_SUCCESS);
}
