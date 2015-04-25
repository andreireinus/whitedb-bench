#include <whitedb/dbapi.h>
#include <whitedb/index.h>
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
    wg_int lock_id;

    db = wg_attach_database(name, 57671680);  // 55 megabytes
    if (!db) {
        printf("failed at %d\n", nr);
        return -1;
    }

    wg_create_index(db, 2, WG_INDEX_TYPE_TTREE, NULL, 0);

    lock_id = wg_start_write(db);
    if (!lock_id) {
        return -1;
    }
    for (i = 0; i < 68000; i++) {
        record = wg_create_record(db, field_count);
        for (j = 0; j < field_count; j++) {
            wg_set_int_field(db, record, j, j * i);
        }
    }
    wg_end_write(db, lock_id);

    lock_id = wg_start_read(db)


    wg_detach_database(db);
    wg_delete_database(name);
    return 0;
}
