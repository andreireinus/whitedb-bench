import time
import wgdb
import csv

def runTest():
	count = 0
	q = wgdb.make_query(db, arglist=[(0,wgdb.COND_EQUAL,"ee")])

	record = wgdb.fetch(db, q);
	while record is not None:
		count += 1
		try:
			record = wgdb.fetch(db, q);
		except wgdb.error:
			record = None

db = wgdb.attach_database("1", 1073741824);

for i in range(0,20):
	start = time.clock();
	runTest();
	print time.clock() - start

wgdb.detach_database(db);
wgdb.delete_database("1");