/*
*CSCD 305 CPP HW#4 - LinkedList of Linking Things All Together and Stuff
*By: Bin Mei
*Aug 15th, 2014
*Compiler Used: VS CL only, G++ error message included.
*Time took:
*	Attending Bruno Mars concert & Birthday Weekend with the Girl Friend - 47 hour	
*   Learning C++ Template - 3 hours
* 	Playing with C++ Template - 2 hours
*	Coding - 15+ hours
*	Debugging - 5+ hours
*	Freaking out about the Iterator wtf - 4 hours
*   Freaking out about the Design Pattern Project - 15 hours
*	Debugging/Make everything Pretty - //yolo
*Description of Classes:
*	main - test methods
*/

#include <cstdio>
#include <iostream>
#include <list>

#include "ListException.h"
#include "LinkedList.h"
#include "CBigInteger.h"

using namespace std;

void testforprimitives()
{
	cout << "Float List Test" << endl;
	cout << "Create new List(list1):" << endl;

	LinkedList<float> list1;
	list1.add(2);
	list1.add(3);
	cout << list1 << endl;

	cout << "Copy Constructor and add function Test(list2)" << endl;
	LinkedList<float> list2(list1);
	list2.add(3.2);
	cout << list2;

	cout << "== operator test" << endl;
	cout << (list1 == list2) << endl;

	cout << "+ operator and Move Constructor Test(list3)" << endl;
	LinkedList<float> list3 = list1 + list2;
	cout << list3;

	cout << "Remove Test" << endl;
	list3.remove(3.0f);
	cout << list3 << endl;
	list3.removewithindex(2);
	cout << list3 << endl;
	list3.removewithindex(5);

	list3.add(2.3f);
	list3.add(2.5f);
	list3.add(1.3f);
	list3.add(0.3f);
	list3.add(4, 1.7f);
	cout << list3;

	cout << "Sort Test" << endl;
	list3.sort();
	cout << list3;
}

void testforuserdefinewithMenu()
{
	CBigInteger a ("12312321312");
	CBigInteger b ("21312321");
	CBigInteger c ("3213213");
	CBigInteger d ("323213123213");
	CBigInteger e ("2131232321");

	LinkedList<CBigInteger> list1;
	list1.add(a);
	list1.add(b);
	list1.add(0, c);
	cout << "Constructor and Add Method" << endl;
	cout << list1 << endl;

	LinkedList<CBigInteger> list2(list1);
	cout << "Copy constructor" << endl;
	cout << list2 << endl;

	LinkedList<CBigInteger> list3 = list1 + list2;
	cout << "Move constructor and operator +" << endl;
	cout << list3 << endl;

	cout << list3 << endl;

	while (true){
		
		cout << "1. Get BigInteger with index." << endl;
		cout << "2. Add BigInteger to list." << endl;
		cout << "3. Add BigInteger to list with index." << endl;
		cout << "4. Remove BigInteger from list." << endl;
		cout << "5. Remove BigInteger from list with index." << endl;
		cout << "6. Sort list." << endl;
		cout << "7. Print all values." << endl;
		cout << "8. Delete all values." << endl;
		cout << "9. Exit." << endl;
		int option = 8;
		cout << "Select option : ";
		cin >> option;
		switch (option){
		case 1:
			{
						int index;
						cout << "Input index : ";
						cin >> index;
						CBigInteger val = list3[index];
						cout << val << endl;
			}
			break;
		case 2:
			{
						string strval;
						cout << "Input BigInteger : ";
						cin >> strval;
						CBigInteger val (strval.c_str());
						list3.add(val);
						cout << "Added." << endl;
			}
			break;
		case 3:
			{
						string strval;
						int index;
						cout << "Input BigInteger : ";
						cin >> strval;
						cout << "Input index : ";
						cin >> index;
						CBigInteger val (strval.c_str());
						list3.add(index, val);
						cout << "Added." << endl;
			}
			break;
		case 4:
			{
						string strval;
						cout << "Input BigInteger : ";
						cin >> strval;
						CBigInteger val (strval.c_str());
						list3.remove(val);
						cout << "Removed." << endl;
			}
			break;
		case 5:
			{
						int index;
						cout << "Input index : ";
						cin >> index;
						CBigInteger val = list3.removewithindex(index);
						cout << "Removed BigInteger Val : " << val << endl;
			}
			break;
		case 6:
			{
						list3.sort();
						cout << "List is sorted." << endl;
			}
			break;
		case 7:
			{
						LinkedList<CBigInteger>::iterator it;
						cout << "List Values:" << endl;
// 						for (it = list3.begin(); it != list3.end(); ++it){
// 							cout << "\t" << *it << endl;
// 						}
						for (CBigInteger tmp : list3){
							cout << "\t" << tmp << endl;
						}
						cout << endl;
			}
			break;
		case 8:
			{
						list3.deleteAll();
						cout << "All values are now deleted."<<endl;
						break;
			}
		default:
			break;
		}
		if (option == 9){
			break;
		}
	}
}

int main(int argc, char* argv[])
{
	testforprimitives();
	testforuserdefinewithMenu();
	return 0;
}

