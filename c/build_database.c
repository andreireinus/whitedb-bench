#include <whitedb/dbapi.h>
#include <whitedb/indexapi.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "query_utils.h"

int main(int argc, char **argv) {
	void *db;

	db = build_city_database();
	printf("argc: %d\n", argc);
	if (argc > 1) {
		wg_create_index(db, 0, WG_INDEX_TYPE_TTREE, NULL, 0);
	}

	return (EXIT_SUCCESS);
}