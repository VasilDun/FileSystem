// FileSystem.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#define JSON_SPIRIT_VALUE_ENABLED
#define JSON_SPIRIT_WVALUE_ENABLED
#define JSON_SPIRIT_MVALUE_ENABLED
#define JSON_SPIRIT_WMVALUE_ENABLED

#include"reader.h"
#include"writer.h"
#include"value.h"
#include <fstream>
#include <time.h>
#include "pch.h"
#include <iostream>
#include<string>
#include<filesystem>
#include "dirent.h"
#include<sys/stat.h>
#include<string.h>
#include<stdio.h>
using namespace std;

        
bool trigger = true;
string current="";

long GetFileSize(std::string filename)
{
	struct stat stat_buf;
	int rc = stat(filename.c_str(), &stat_buf);
	return rc == 0 ? stat_buf.st_size : -1;
}

void explore(char *dir_name) {
	DIR *dir;
	struct dirent *entry;
	struct stat info;

	dir = opendir(dir_name);
	if (!dir) {
		cout << " Directory was not found\n" << endl;
		return;
	}

	while((entry = readdir(dir)) != NULL)
	{
		if (entry->d_name[0] != '.')
		{
			string path = string(dir_name) + "/" + string(entry->d_name);
			if (trigger)
			{
				cout << "Name: " << string(dir_name) << endl;
				trigger = false;
				current = string(entry->d_name);
				struct stat attrib;

				stat(path.c_str(), &attrib); 				
				struct tm* clock = gmtime(&(attrib.st_mtime));
				cout << "Date Created: " << asctime(clock) << endl;

				cout << "Files: ";
			}
			cout << "Name: " << string(entry->d_name) << endl;
			struct stat attrib;

			stat(path.c_str(), &attrib);
			struct tm* clock = gmtime(&(attrib.st_mtime));
			cout << "Date Created: " << asctime(clock);		
			cout << "Size: " << GetFileSize(path) << endl;
			cout << "--------------------------------" << endl;

			//size_t found = string(path).find(current);
			//if (found == string::npos)
			//{
			//	cout << "Children: " << current << endl;

			//	size_t found1 = string(string(entry->d_name)).find(".");
			//	if (found1 == string::npos) {
			//		current = string(entry->d_name);
			//	}
			//	else {
			//	//add info about file to JSON
			//	}
			//}

			stat(path.c_str(),&info);
			if (S_ISDIR(info.st_mode))
			{
				explore((char*)path.c_str());
			}
		}
	}
	closedir(dir);
}

int main() {
	//Object addr_obj;

	//addr_obj.push_back(Pair("house_number", 42));
	//addr_obj.push_back(Pair("road", "East Street"));
	//addr_obj.push_back(Pair("town", "Newtown"));

	//ofstream os("address.txt");
	//os.write(addr_obj, os, pretty_print);
	//os.close();



	explore((char*)".");
	

	return 0;
}

