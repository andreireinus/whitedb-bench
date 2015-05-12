#!/bin/sh

sh compile.sh
./insert_case1.bin > ../results/c__insert_case1.log
./insert_case2.bin > ../results/c__insert_case2.log
./update_case.bin > ../results/c__update_case.log
./query_case1.bin > ../results/c__query_case1.log
./query_case2.bin > ../results/c__query_case2.log
./query_case3.bin > ../results/c__query_case3.log