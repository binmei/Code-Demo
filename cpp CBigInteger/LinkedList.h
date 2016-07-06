/*
*CSCD 305 CPP HW#4 - LinkedList of Linking Things All Together and Stuff
*By: Bin Mei
*Aug 15th, 2014
*Compiler Used: VS CL only, G++ error message included.
*Description of Classes:
*	main - test methods
*/


#ifndef _LINKEDLIST_H_
#define _LINKEDLIST_H_

#include <iostream>

#include "ListException.h"

using namespace std;

template <class T>
class LinkedList
{
	// Nested class
protected:

	class Node
	{
	public:
		Node(T data, Node* next = NULL) : data(data), next(next){}
		~Node(){};

		T data;
		Node* next;
	};

public:

	class iterator
	{
	private:
		Node* current;
		iterator(Node* x) :current(x){}
	public:

		friend class LinkedList<T>;

		iterator(){}
		bool operator == (const iterator& x) const{
			return current == x.current;
		}

		bool operator != (const iterator& x) const{
			return current != x.current;
		}

		T& operator*() const{ return current->data; }

		iterator& operator++(){
			current = current->next;
			return *this;
		}

		iterator operator++(int){
			iterator tmp = *this;
			++(*this);
			return tmp;
		}
	};

	friend class iterator;

public:

	LinkedList();
	LinkedList(LinkedList<T>& list);
	explicit LinkedList(LinkedList<T>&& list);
	~LinkedList();
	
	T operator [] (unsigned int index);
	LinkedList<T>& operator=(const LinkedList<T>& list);
	LinkedList<T>& operator=(LinkedList<T>&& list);
	bool operator==(LinkedList<T>& list);
	LinkedList<T> operator +(LinkedList<T>& list);
	friend ostream& operator<<(ostream& os, LinkedList<T>& list){
		int i = 0;
		os << "List Content Count : " << list.count << endl;
		for (iterator it = list.begin(); it != list.end(); ++it){
			os << "\tElement " << i << " : " << *it << endl;
			i++;
		}
		return os;
	}

	void add(T val);
	void add(unsigned int index, T val);
	bool remove(T val);
	T removewithindex(unsigned int index);
	void deleteAll();

	void sort();

public:
	bool isempty();
	unsigned int getcount();
	iterator begin();
	iterator end();
	
protected:
	iterator getpos(unsigned int index);
	iterator findposwithval(T val);
	void swap(unsigned int index1, unsigned int index2);

protected:
	unsigned int count;
	Node* header;
};

#include "LinkedList.cpp"

#endif