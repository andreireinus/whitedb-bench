#!/bin/sh
[ -z "$CC" ] && CC="cc"

$CC -O3 -o insert_case1.bin insert_case1.c utils.c -lwgdb -lrt


$CC -O3 -o query_case3.bin query_case3.c utils.c -lwgdb -lrt
