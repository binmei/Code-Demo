/*
*CSCD 305 CPP HW#4 - LinkedList of Linking Things All Together and Stuff
*By: Bin Mei
*Aug 15th, 2014
*Compiler Used: VS CL only, G++ error message included.
*Description of Classes:
*	main - test methods
*/

#ifndef CBIGINTEGER_H
#define	CBIGINTEGER_H

#include <iostream>
#include <cstring>
#include <cctype>

using namespace std;

class CBigInteger {

public:
    friend ostream& operator<<(ostream& out, const CBigInteger& CBigInteger);
    friend istream& operator>>(istream& in, CBigInteger& CBigInteger);
    friend bool operator==(const CBigInteger& lhs, const CBigInteger& rhs);
    friend bool operator!=(const CBigInteger& lhs, const CBigInteger& rhs);
    friend bool operator>(const CBigInteger& lhs, const CBigInteger& rhs);
    friend bool operator>=(const CBigInteger& lhs, const CBigInteger& rhs);
    friend bool operator<(const CBigInteger& lhs, const CBigInteger& rhs);
    friend bool operator<=(const CBigInteger& lhs, const CBigInteger& rhs);

    friend CBigInteger operator*(const CBigInteger lhs, const CBigInteger& rhs); //Can't use for loop add method!!!
    friend CBigInteger operator/(const CBigInteger lhs, const CBigInteger& rhs); //Can't use for loop add method!!!
    friend CBigInteger operator+(const CBigInteger lhs, const CBigInteger& rhs);
    friend CBigInteger operator-(const CBigInteger lhs, const CBigInteger& rhs);

public:

    CBigInteger();
    CBigInteger(const CBigInteger& original);
    CBigInteger(string num);
    CBigInteger(string num, bool sign);
    CBigInteger(char *num);
    CBigInteger(char *num, bool sign);
    CBigInteger(int n);

    CBigInteger operator%(const CBigInteger& rhs);
    CBigInteger& operator=(const CBigInteger& arg);
    CBigInteger& operator=(const int& arg);

    int sizeOf() const;

    ~CBigInteger();

private:
    int size;
    bool sign; //if sign==true, it means number is NEGATIVE
    char *number;
};

#endif	/* CBIGINTEGER_H */

