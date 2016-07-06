/*
*CSCD 305 CPP HW#4 - LinkedList of Linking Things All Together and Stuff
*By: Bin Mei
*Aug 15th, 2014
*Compiler Used: VS CL only, G++ error message included.
*Description of Classes:
*	main - test methods
*/

#pragma once

#include <exception>
#include <string>

using namespace std;

class ListException : public exception
{
public:
	ListException(string errorstr) : _errorstr(errorstr){}
	virtual ~ListException() throw(){};

	virtual const char* what(){ return _errorstr.c_str(); }
protected:
	string _errorstr;
};

