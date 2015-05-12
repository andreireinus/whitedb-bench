import time
import wgdb

field_count = 100

def runTest():
	record = wgdb.get_first_record(db);
	while record is not None:
		for j in range(0,field_count):
			wgdb.set_field(db, record, j, j + 1);
		try:
			record = wgdb.get_next_record(db, record);
		except wgdb.error:
			record = None

db = wgdb.attach_database("1", 1073741824);

for x in range(0, 680000):
	record = wgdb.create_record(db, field_count)
	for j in range(0,field_count):
		wgdb.set_field(db, record, j, j)

for i in range(0,20):
	start = time.clock();
	runTest();
	print time.clock() - start

wgdb.detach_database(db);
wgdb.delete_database("1");
