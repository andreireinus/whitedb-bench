import time
import wgdb
import csv

def runTest():
	count = 0
	record = wgdb.get_first_record(db);
	while record is not None:
		country = wgdb.get_field(db, record, 0);
		
		if country == "ee":
			count += 1
		
		try:
			record = wgdb.get_next_record(db, record);
		except wgdb.error:
			record = None

db = wgdb.attach_database("1", 1073741824);

with open('../worldcitiespop.txt', 'rb') as csvfile:
	reader = csv.reader(csvfile, delimiter=',', quotechar='|')
	firstRow = True
	for row in reader:
		if firstRow:
			firstRow = False
			continue
			
		record = wgdb.create_record(db, 5)
		wgdb.set_field(db, record, 0, row[0])
		wgdb.set_field(db, record, 1, row[1])
		wgdb.set_field(db, record, 2, row[2])
		wgdb.set_field(db, record, 3, row[3])
		wgdb.set_field(db, record, 4, row[4])

for i in range(0,20):
	start = time.clock();
	runTest();
	print time.clock() - start

wgdb.detach_database(db);
wgdb.delete_database("1");