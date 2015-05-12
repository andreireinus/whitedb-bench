#!/bin/sh
[ -z "$CC" ] && CC="cc"

$CC -O3 -o insert_case1.bin insert_case1.c utils.c -lwgdb -lrt
$CC -O3 -o insert_case2.bin insert_case2.c utils.c -lwgdb -lrt
$CC -O3 -o update_case.bin update_case.c utils.c -lwgdb -lrt
$CC -O3 -o query_case1.bin query_case1.c query_utils.c utils.c -lwgdb -lrt
$CC -O3 -o query_case2.bin query_case2.c query_utils.c utils.c -lwgdb -lrt
$CC -O3 -o query_case3.bin query_case3.c query_utils.c utils.c -lwgdb -lrt
$CC -O3 -o ../python/build_database build_database.c query_utils.c utils.c -lwgdb -lrt