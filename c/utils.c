#include <time.h>
#include <stdio.h>

int run_count() {
    return 50;
}

int diff(struct timespec start, struct timespec end) {
    double d = (end.tv_sec - start.tv_sec) + ((double)(end.tv_nsec - start.tv_nsec) / (double)1000000000);
    printf("%f\n", d);

    return 0;
}