/*
*CSCD 305 CPP HW#4 - LinkedList of Linking Things All Together and Stuff
*By: Bin Mei
*Aug 15th, 2014
*Compiler Used: VS CL only, G++ error message included.
*Description of Classes:
*	main - test methods
*/

#ifndef _LINKEDLIST_CPP_
#define _LINKEDLIST_CPP_

#include "LinkedList.h"

template <class T>
LinkedList<T>::LinkedList()
{
	header = NULL;
	count = 0;
	cout << "Constructor is called" << endl;
}

template <class T>
LinkedList<T>::LinkedList(LinkedList<T>& list)
{
	cout << "Copy constructor is called" << endl;
	header = NULL;
	count = 0;
	LinkedList<T>::iterator it;
	Node* psrc = NULL;
	for (it = list.begin(); it != list.end(); ++it){
		add(*it);
	}
}

template <class T>
LinkedList<T>::LinkedList(LinkedList<T>&& list)
{
	cout << "Move Constructor is called" << endl;
	header = list.header;
	count = list.getcount();
	list.header = NULL;
	list.count = 0;
}

template <class T>
LinkedList<T>::~LinkedList()
{
	deleteAll();
	count = 0;
}

template <class T>
T LinkedList<T>::operator[](unsigned int index)
{
	try{
		int count = getcount();
		if (count == 0){
			throw ListException("List is empty.");
		}

		if (index >= count){
			throw ListException("Out of Range.");
		}

		Node* tmp = getpos(index).current;

		if (tmp == NULL){
			throw ListException("Unknown Error.");
		}

		return tmp->data;
	}
	catch (ListException e){
		cout << e.what() << endl;
	}
}

template <class T>
LinkedList<T>& LinkedList<T>::operator = (const LinkedList<T>& list)
{
	deleteAll();

	header = NULL;
	count = 0;
	LinkedList<T>::iterator it;
	Node* psrc = NULL;
	for (it == list.begin(); it != list.end(); ++it){
		add(*it);
	}
	
	return *this;
}

template <class T>
LinkedList<T>& LinkedList<T>::operator = (LinkedList<T>&& list)
{
	header = list.header;
	count = list.getcount();
	list.header = NULL;
	list.count = 0;
	return *this;
}

template <class T>
bool LinkedList<T>::operator == (LinkedList<T>& list)
{
	if (getcount() != list.getcount()) return false;

	LinkedList<T>::iterator it, it1;
	it1 = list.begin();
	for (it = begin(); it != end(); ++it){
		if (*it != *it1)
			return false;
		++it1;
	}

	return true;
}

template <class T>
LinkedList<T> LinkedList<T>::operator + (LinkedList<T>& list)
{
	LinkedList<T> result(*this);
	for (iterator it = list.begin(); it != list.end(); it++){
		result.add(*it);
	}
	return LinkedList<T>(std::move(result));
}

template <class T>
void LinkedList<T>::add(T val)
{
	if (header == NULL){
		header = new Node(val);
	}
	else {
		LinkedList<T>::iterator it = begin();
		for (unsigned int i = 0; i < count - 1; i++)
			++it;
		Node* paddednode = new Node(val);
		it.current->next = paddednode;
	}
	count++;
}

template <class T>
void LinkedList<T>::add(unsigned int index, T val)
{
	try{
		if (index >= getcount())
			throw ListException("Out of Range.");

		Node* ptmpNode = new Node(val);
		if (index == 0){
			ptmpNode->next = header;
			header = ptmpNode;
		}
		else {
			iterator insertedpos = getpos(index - 1);
			Node* ptmpNode = new Node(val);
			ptmpNode->next = insertedpos.current->next;
			insertedpos.current->next = ptmpNode;
		}
		count++;
	}
	catch (ListException e){
		cout << e.what() << endl;
	}
}

template <class T>
bool LinkedList<T>::remove(T val)
{
	iterator removepos = findposwithval(val);
	if (removepos == end())
		return false;

	if (removepos.current == header){
		Node* ptmp = header;
		header = header->next;
		delete ptmp;
		count--;
		return true;
	}

	iterator it = begin();
	while (it != end()){
		iterator it1 = it;
		if (++it1 == removepos){
			it.current->next = removepos.current->next;
			delete removepos.current;
			count--;
			break;
		}
		++it;
	}

	return true;
}

template<class T>
T LinkedList<T>::removewithindex(unsigned int index)
{
	try{
		if (index >= getcount()){
			throw ListException("Out of Range.");
		}

		if (index == 0){
			T val = header->data;
			Node* tmp = header;
			header = header->next;
			delete tmp;
			count--;
			return val;
		}

		iterator previt = getpos(index - 1);
		Node* premovednode = previt.current->next;
		T val = premovednode->data;
		previt.current->next = premovednode->next;
		delete premovednode;
		count--;
		return val;
	}
	catch (ListException e){
		cout << e.what() << endl;
	}
}

template <class T>
void LinkedList<T>::deleteAll()
{
	Node* tmp;
	while (header != NULL){
		tmp = header;
		header = header->next;
		delete tmp;
	}
	count = 0;
	header = NULL;
}

template <class T>
void LinkedList<T>::sort()
{
	int count = getcount();
	for (int i = 0; i < count - 1; i++){
		for (int j = i + 1; j < count; j++){
			iterator it1, it2;
			it1 = getpos(i);
			it2 = getpos(j);
			if (*it1 > *it2){
				swap(i, j);
			}
		}
	}
}

template <class T>
bool LinkedList<T>::isempty()
{
	return header == NULL;
}

template <class T>
unsigned int LinkedList<T>::getcount()
{
	return count;
}

template <class T>
typename LinkedList<T>::iterator LinkedList<T>::begin()
{
	return iterator(header);
}

template <class T>
typename LinkedList<T>::iterator LinkedList<T>::end()
{
	return iterator(NULL);
}


template <class T>
typename LinkedList<T>::iterator LinkedList<T>::getpos(unsigned int index)
{
	if (index >= getcount())
		return NULL;

	iterator it = begin();
	for (unsigned int i = 0; i < index ; i++){
		++it;
	}

	return it;
}

template <class T>
typename LinkedList<T>::iterator LinkedList<T>::findposwithval(T val)
{
	iterator it = begin();
	while (it != end()){
		if (*it == val)
			return it;
		it++;
	}
	return end();
}

template <class T>
void LinkedList<T>::swap(unsigned int index1, unsigned int index2)
{
	Node *prevnode1, *prevnode2;
	Node *node1, *node2;
	
	unsigned int count = getcount();
	if (index1 < 0 || index1 >= count || index2 < 0 || index2 >= count || index1 == index2)
		return;

	prevnode1 = prevnode2 = NULL;
	node1 = header;
	node2 = header;
	
	if (index1 > 0){
		prevnode1 = getpos(index1 - 1).current;
		node1 = prevnode1->next;
	} 

	if (index2 > 0){
		prevnode2 = getpos(index2 - 1).current;
		node2 = prevnode2->next;
	}

	if (prevnode1 == NULL){
		Node* tmp = node2->next;
		prevnode2->next = node1;
		node2->next = node1->next;
		node1->next = tmp;
		header = node2;
	}
	else if (prevnode2 == NULL){
		Node* tmp = prevnode1->next;
		prevnode1->next = node2;
		node1->next = node2->next;
		node2->next = tmp;
		header = node1;
	}	else if (node1->next == node2){
		Node* tmp = node2->next;
		node2->next = node1;
		prevnode1->next = node2;
		node1->next = tmp;
	} else {
		Node* tmp2 = node2->next;
		Node* tmp1 = node1->next;
		prevnode2->next = node1;
		node1->next = tmp2;
		prevnode1->next = node2;
		node2->next = tmp1;
	}
}
#endif