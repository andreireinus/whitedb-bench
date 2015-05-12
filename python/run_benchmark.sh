python ./insert_case1.py > ../results/python__insert_case1.log
python ./insert_case2.py > ../results/python__insert_case2.log
python ./query_case1.py > ../results/python__query_case1.log
python ./update_case.py > ../results/python__update_case.log

./build_database
python ./query_case2.py > ../results/python__query_case2.log
./build_database with_index
python ./query_case3.py > ../results/python__query_case3.log