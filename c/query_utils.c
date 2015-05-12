#include <whitedb/dbapi.h>
#include <stdio.h>
#include <stdlib.h>

void *build_city_database() {
    void *db;
    void *record;
    char *name = "1";
    char *buffer;
    char *temp;
    int count;
    FILE *fp;

    db = wg_attach_database(name, 1073741824);  // 1 gigabyte
    if (!db) {
        printf("failed at create database\n");
        exit(0);
    }

    fp = fopen("../worldcitiespop.txt", "r");
    if (!fp) {
        printf("failed to open cities file\n");
        exit(0);
    }

    buffer = malloc(255);
    while (fgets(buffer, 255, fp) != NULL) {
        count++;
        if (count == 1) {
            continue;
        }

        record = wg_create_record(db, 5);

        temp = strtok(buffer, ",");
        wg_set_str_field(db, record, 0, temp); // Country

        temp = strtok(NULL, ",");
        wg_set_str_field(db, record, 1, temp); // City

        temp = strtok(NULL, ",");
        wg_set_str_field(db, record, 2, temp); // AccentCity

        temp = strtok(NULL, ",");
        wg_set_str_field(db, record, 3, temp); // Region

        temp = strtok(NULL, ",");
        wg_set_int_field(db, record, 4, atoi(temp)); // Population
    }
//    printf("Count:> %d\n", count);

    fclose(fp);
    return db;
}