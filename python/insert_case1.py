import time
import wgdb

def runTest():
	field_count = 100
	db = wgdb.attach_database("1", 57671680);
	for x in range(0, 68000):
		record = wgdb.create_record(db, field_count)
		for j in range(0,field_count):
			wgdb.set_field(db, record, j, j)
	wgdb.detach_database(db);
	wgdb.delete_database("1");
	return;
	
def printDiff(start, end):
	diff = end - start;
	
	
for i in range(0,10):
	start = time.clock();
	runTest();
	print 'Time in function', time.clock() - start
	
