import time
import wgdb

def runTest():
	field_count = 100
	db = wgdb.attach_database("1", 1073741824);
	for x in range(0, 680000):
		record = wgdb.create_record(db, field_count)
		for j in range(0,field_count):
			wgdb.set_field(db, record, j, j)
	wgdb.detach_database(db);
	wgdb.delete_database("1");
	
for i in range(0,20):
	start = time.clock();
	runTest();
	print time.clock() - start
	
