#include <whitedb/dbapi.h>
#include <whitedb/indexapi.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "query_utils.h"

int run_test(void *db, int nr) {
    wg_query_arg arglist[5];
    wg_query *query;
    wg_int matchrec[10];
    int count;
    wg_int lock_id;

    arglist[0].column = 0;
    arglist[0].cond = WG_COND_EQUAL;
    arglist[0].value = wg_encode_query_param_str(db, "ee", NULL);

    lock_id = wg_start_read(db);
    if (!lock_id) {
        fprintf(stderr, "failed to get read lock, aborting.\n");
        return 1;
    }

    query = wg_make_query(db, NULL, 0, arglist, 1);
    if (!query) {
        fprintf(stderr, "failed to build query, aborting.\n");
        return 2;
    }

    void *rec = wg_fetch(db, query);
    while (rec) {
        count++;
        rec = wg_fetch(db, query);
    }
    wg_end_read(db, lock_id);
    wg_free_query(db, query);

    return (count == 0) ? 1 : 0;
}

int main(int argc, char **argv) {
    void *db;
	struct timespec start, stop;
    int i;

	db = build_city_database();

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

