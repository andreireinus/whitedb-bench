#include <whitedb/dbapi.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

int run_test(int nr);

int main(int argc, char **argv) {
    struct timespec start, stop;
    int i;

    for (i = 0; i < 10; i++) {
        clock_gettime(CLOCK_REALTIME, &start);
        run_test(i);
        clock_gettime(CLOCK_REALTIME, &stop);
        diff(start, stop);
    }
    return (EXIT_SUCCESS);
}


int run_test(int nr) {
    void *db;
    void *record;
    char *name = "1";
    int i, j;
    int field_count = 100;

    db = wg_attach_database(name, 57671680);  // 55 megabytes
    if (!db) {
        printf("failed at %d\n", nr);
        exit(0);
    }
    for (i = 0; i < 68000; i++) {
        record = wg_create_record(db, field_count);
        for (j = 0; j < field_count; j++) {
            wg_set_int_field(db, record, j, j);
        }
    }

    wg_detach_database(db);
    wg_delete_database(name);
    return 0;
}
