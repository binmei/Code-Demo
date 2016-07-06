/*
*CSCD 305 CPP HW#4 - LinkedList of Linking Things All Together and Stuff
*By: Bin Mei
*Aug 15th, 2014
*Compiler Used: VS CL only, G++ error message included.
*Description of Classes:
*	main - test methods
*/

#include<malloc.h>
#include<string>
#include "CBigInteger.h"

int CBigInteger::sizeOf() const {
    return this->size * sizeof (char) + sizeof (int) + sizeof (bool);
}

char *strrev(char *str) {
    char *p1, *p2;

    if (!str || ! *str)
        return str;
    for (p1 = str, p2 = str + strlen(str) - 1; p2 > p1; ++p1, --p2) {
        *p1 ^= *p2;
        *p2 ^= *p1;
        *p1 ^= *p2;
    }
    return str;
}

CBigInteger::CBigInteger() {
    number = (char*) malloc(2 * sizeof (char));
    number[0] = '0';
    size = 1;
    sign = false;
    number[size] = '\0';
}

CBigInteger::CBigInteger(string num) {
    sign = false;
    size = num.length();
    if (num.at(0) != '-') {
        number = (char*) malloc((size + 1) * sizeof (char));
        for (int i = 0; i < size; i++) {
            number[i] = num.at(i);
        }
    } else {
        size--;
        sign = true;
				number = (char*)malloc((size + 1) * sizeof (char));
        for (int i = 0; i < size; i++) {
            number[i] = num.at(i + 1);
        }
    }
    if (size == 0) {
        number = (char*) malloc(2 * sizeof (char));
        number[0] = '0';
        number[1] = '\0';
        size = 1;
        sign = false;
    }
    this->number[this->size] = '\0';
}

CBigInteger::CBigInteger(string num, bool sign) {
    size = num.length();
		number = (char*)malloc((size + 1) * sizeof (char));
    this->sign = sign;
    if (num.at(0) == '+' || num.at(0) == '-') {
        size--;
        for (int i = 0; i < size; i++) {
            number[i] = num.at(i + 1);
        }
    } else {
        for (int i = 0; i < size; i++) {
            number[i] = num.at(i);
        }
    }
    if (size == 0) {
        number = (char*) malloc(2 * sizeof (char));
        number[0] = '0';
        number[1] = '\0';
        sign = false;
        size = 1;
    }
    this->number[this->size] = '\0';
}

CBigInteger::CBigInteger(char *num) {
    sign = false;
    size = strlen(num);
    if (num[0] != '-') {
			number = (char*)malloc((size + 1) * sizeof (char));
        for (int i = 0; i < size; i++) {
            number[i] = num[i];
        }
    } else {
        size--;
        sign = true;
				number = (char*)malloc((size + 1) * sizeof (char));
        for (int i = 0; i < size; i++) {
            number[i] = num[i + 1];
        }
    }
    if (size == 0) {
        number = (char*) malloc(2 * sizeof (char));
        number[0] = '0';
        number[1] = '\0';
        sign = false;
        size = 1;
    }
    this->number[this->size] = '\0';
}

CBigInteger::CBigInteger(char *num, bool sign) {
    size = strlen(num);
		number = (char*)malloc((size + 1) * sizeof (char));
    this->sign = sign;

    if (num[0] == '+' || num[0] == '-') {
        size--;
        for (int i = 0; i < size; i++) {
            number[i] = num[i + 1];
        }
    } else {
        for (int i = 0; i < size; i++) {
            number[i] = num[i];
        }
    }
    if (size == 0) {
        number = (char*) malloc(2 * sizeof (char));
        number[0] = '0';
        number[1] = '\0';
        sign = false;
        size = 1;
    }
    this->number[this->size] = '\0';
}

CBigInteger operator+(const CBigInteger lhs, const CBigInteger& rhs) {
    bool change_sign = false;
    if (lhs.sign == true && rhs.sign == true) {
        change_sign = true;
    }
    if (lhs.sign == true && rhs.sign == false) {
        CBigInteger a(lhs);
        CBigInteger b(rhs);
        a.sign = false;
        return b - a;
    }
    if (lhs.sign == false && rhs.sign == true) {
        CBigInteger a(rhs);
        a.sign = false;
        return (lhs)-(a);
    }

    int size = lhs.size;
    if (rhs.size > size) {
        size = rhs.size;
    }
    size++;

    char *addResult = (char*) malloc(size * sizeof (char));
    int index1, index2, indexResult;
    index1 = lhs.size - 1;
    index2 = rhs.size - 1;
    indexResult = size - 1;

    unsigned char x1, x2, xrem = 0, xadd;
    while ((index1 >= 0)&&(index2 >= 0)) {
        x1 = lhs.number[index1] - '0';
        x2 = rhs.number[index2] - '0';
        xadd = x1 + x2 + xrem;
        xrem = xadd / 10;
        xadd %= 10;

        addResult[indexResult] = xadd + '0';

        indexResult--;
        index1--;
        index2--;
    }
    while (index1 >= 0) {
        x1 = lhs.number[index1] - '0';
        xadd = x1 + xrem;
        xrem = xadd / 10;
        xadd %= 10;

        addResult[indexResult] = xadd + '0';

        indexResult--;
        index1--;
    }
    while (index2 >= 0) {
        x2 = rhs.number[index2] - '0';
        xadd = x2 + xrem;
        xrem = xadd / 10;
        xadd %= 10;

        addResult[indexResult] = xadd + '0';

        indexResult--;
        index2--;
    }
    if (xrem > 0) {
        addResult[indexResult] = xrem + '0';
        indexResult--;
    }

    for (int j = 0; j < size; j++) {
        if ((addResult[j] < '0') || (addResult[j] > '9')) {
            for (int i = j; i < size - 1; i++) {
                addResult[i] = addResult[i + 1];
            }
            j--;
            size--;
        }
    }
    while ((size > 0)&&((addResult[size - 1] < '0') || (addResult[size - 1] > '9'))) {
        size--;
    }

    char *addResultFinal = (char*) malloc(size * sizeof (char));
    int ind = 0;
    while (ind < size) {
        addResultFinal[ind] = addResult[ind];
        ind++;
    }
    addResultFinal[size] = '\0';
    //free(addResult);


    CBigInteger a;
    a.size = size;
    a.number = (char*) malloc((a.size + 1) * sizeof (char));
    for (int i = 0; i < a.size; i++)
        a.number[i] = addResultFinal[i];
    a.sign = change_sign;
    a.number[a.size] = '\0';

    return a;
}

CBigInteger operator-(const CBigInteger lhs, const CBigInteger& rhs) {
    bool resultIsPositive = true;
    int size = lhs.size;

    CBigInteger *left, *right;

    if (lhs.sign == true) {
        if (rhs.sign == true) { // -a - (-b) === -a + b === b-a
            CBigInteger a(lhs.number, false);
            CBigInteger b(rhs.number, false);
            return b - a;
        } else { // -a -b = -(a+b)
            CBigInteger a(lhs.number, false);
            CBigInteger b(rhs.number, false);
            CBigInteger d = a + b;
            d.number[d.size] = '\0';

            return *(new CBigInteger(d.number, true));
        }
    } else {
        if (rhs.sign == false) {// a - b
            CBigInteger a(lhs.number, false);
            CBigInteger b(rhs.number, false);
            if (rhs.size > size) {
                resultIsPositive = false;

                CBigInteger d = b - a;
                d.number[d.size] = '\0';
                d.sign = true;

                return *(new CBigInteger(d.number, true));
            } else if (rhs.size == size && a < b) {
                CBigInteger d = b - a;
                d.number[d.size] = '\0';

                return *(new CBigInteger(d.number, true));
            }
        }
        if (rhs.sign == true) { // a - (-b) = a+b
            CBigInteger a(lhs.number, false);
            CBigInteger b(rhs.number, false);
            return a + b;
        }

    }

    if (rhs.size > size) {
        size = rhs.size;
    }

    char *minusResultFinal = NULL;

    char *minusResult = (char*) malloc(size * sizeof (char));
    int index1, index2, indexResult;

    index1 = lhs.size - 1;
    index2 = rhs.size - 1;
    indexResult = size - 1;

    char x1, x2, xtake = 0, xminus;
    while ((index1 >= 0)&&(index2 >= 0)) {
        x1 = lhs.number[index1] - '0';
        x2 = rhs.number[index2] - '0';
        xminus = x1 - x2 - xtake;
        if (xminus < 0) {
            xtake = 1;
            xminus += 10;
        } else {
            xtake = 0;
        }

        minusResult[indexResult] = xminus + '0';

        indexResult--;
        index1--;
        index2--;
    }
    while (index1 >= 0) {
        x1 = lhs.number[index1] - '0';
        xminus = x1 - xtake;
        if (xminus < 0) {
            xtake = 1;
            xminus += 10;
        } else {
            xtake = 0;
        }

        minusResult[indexResult] = xminus + '0';

        indexResult--;
        index1--;
    }

    for (int j = 0; j < size; j++) {
        if ((minusResult[j] < '0') || (minusResult[j] > '9')) {
            for (int i = j; i < size - 1; i++) {
                minusResult[i] = minusResult[i + 1];
            }
            j--;
            size--;
        }
    }
    while ((size > 0)&&((minusResult[size - 1] < '0') || (minusResult[size - 1] > '9'))) {
        size--;
    }

    minusResultFinal = (char*) malloc(size * sizeof (char));
    int ind = 0;
    while (ind < size) {
        minusResultFinal[ind] = minusResult[ind];
        ind++;
    }
    minusResultFinal[size] = '\0';
    //    free(minusResult);

    CBigInteger a;
    a.size = size;
    a.number = (char*) malloc((a.size + 1) * sizeof (char));
    for (int i = 0; i < a.size; i++)
        a.number[i] = minusResultFinal[i];
    a.sign = !resultIsPositive;
    a.number[a.size] = '\0';

    return a;
}

ostream& operator<<(ostream& out, const CBigInteger& x) {
    if (x.sign == false) {
        out << x.number;
    } else {
        out << "-" << x.number;
    }

    return out;
}

istream& operator>>(istream& in, CBigInteger& x) {
    string n;
    in >> n;
    x = *(new CBigInteger(n));

    return in;
}

bool operator==(const CBigInteger& lhs, const CBigInteger& rhs) {
    if (lhs.sign != rhs.sign) {
        return false;
    }
    int index1, index2;
    index1 = 0;
    index2 = 0;
    if (lhs.size == rhs.size) {
        while (index1 < lhs.size) {
            if (lhs.number[index1] != rhs.number[index1])
                return false;
            index1++;
        }
    } else {
        while ((index1 < lhs.size) && (lhs.number[index1] == '0')) {
            index1++;
        }
        while ((index2 < rhs.size) && (rhs.number[index2] == '0')) {
            index2++;
        }
        if (lhs.size - index1 != rhs.size - index2) {
            return false;
        }
        while (index1 < lhs.size) {
            if (lhs.number[index1] != rhs.number[index2]) {
                return false;
            }
            index1++;
            index2++;
        }
    }
    return true;
}

bool operator!=(const CBigInteger& lhs, const CBigInteger& rhs) {
    if (lhs == rhs) {
        return false;
    }
    return true;
}

bool operator>(const CBigInteger& left, const CBigInteger& right) {
    CBigInteger lhs(left);
    CBigInteger rhs(right);

		while ((lhs.size > 0) && (lhs.number[0] == '0')) {
			for (int i = 0; i < lhs.size - 1; i++) {
				lhs.number[i] = lhs.number[i + 1];
			}
			lhs.size--;
		}

		while ((rhs.size > 0) && (rhs.number[0] == '0')) {
			for (int i = 0; i < rhs.size - 1; i++) {
				rhs.number[i] = rhs.number[i + 1];
			}
			rhs.size--;
		}

    if (lhs.sign == false && rhs.sign == true) {
        return true;
    }
    if (lhs.sign == true && rhs.sign == false) {
        return false;
    }
    if (lhs.sign == false) {
        int index1, index2;
        index1 = lhs.size - 1;
        index2 = rhs.size - 1;
        if (lhs.size == rhs.size) {
            while ((index1 >= 0) && (lhs.number[index1] == rhs.number[index2])) {
                index1--;
                index2--;
            }
            if (index1 < 0 || index2 < 0) {
                return false;
            }
            while (index1 >= 0) {
                if ((unsigned char) (lhs.number[index1] - '0') < (unsigned char) (rhs.number[index2] - '0')) {
                    return false;
                }
                index1--;
                index2--;
            }
            return true;
        } else if (lhs.size < rhs.size) {
            return false;
        } else {
            return true;
        }
    } else {
        int index1, index2;
        index1 = lhs.size - 1;
        index2 = rhs.size - 1;
        if (lhs.size == rhs.size) {
            while ((index1 >= 0) && (lhs.number[index1] == rhs.number[index2])) {
                index1--;
                index2--;
            }
            if (index1 < 0 || index2 < 0) {
                return false;
            }
            while (index1 >= 0) {
                if ((unsigned char) (lhs.number[index1] - '0') > (unsigned char) (rhs.number[index2] - '0')) {
                    return false;
                }
                index1--;
                index2--;
            }
            return true;
        } else if (lhs.size < rhs.size) {
            return true;
        } else {
            return false;
        }
    }

    return true; //won't reach this!
}

bool operator>=(const CBigInteger& lhs, const CBigInteger& rhs) {
    return ((lhs > rhs) || (lhs == rhs));
}

bool operator<(const CBigInteger& lhs, const CBigInteger& rhs) {
    return (!(lhs >= rhs));
}

bool operator<=(const CBigInteger& lhs, const CBigInteger& rhs) {
    return ((lhs < rhs) || (lhs == rhs));
}

CBigInteger& CBigInteger::operator=(const CBigInteger& arg) {
    this->size = arg.size;
    this->sign = arg.sign;
    this->number = (char*) malloc((this->size + 1) * sizeof (char));
    for (int i = 0; i<this->size; i++) {
        this->number[i] = arg.number[i];
    }
    this->number[this->size] = '\0';

    return *this;
}

CBigInteger& CBigInteger::operator=(const int& arg) {
    bool isNegative = arg < 0;
    int x = arg;
    char nr[20];
    int index = 0;
    if (isNegative) {
        x = -x;
    }
    while (x != 0) {
        nr[index++] = x % 10 + '0';
        x /= 10;
    }
    nr[index] = '\0';
    strrev(nr);

    this->size = index;
    this->number = (char*) malloc((this->size + 1) * sizeof (char));
    for (int i = 0; i<this->size; i++)
        this->number[i] = nr[i];
    this->sign = isNegative;
    this->number[this->size] = '\0';

    return *this;
}

CBigInteger::CBigInteger(const CBigInteger& original) {
    size = original.size;
    sign = original.sign;
    number = (char*) malloc((size + 1) * sizeof (char));
    for (int i = 0; i < size; i++) {
        number[i] = original.number[i];
    }
    number[size] = '\0';
}

CBigInteger::CBigInteger(int n) {
    sign = true;
    if (n >= 0) {
        sign = false;
    } else {
        n = -n;
    }
    char t[20];
    int index = 0;
    while (n != 0) {
        t[index] = n % 10 + '0';
        n /= 10;
        index++;
    }
    t[index] = '\0';
    strrev(t);

    size = index;
    number = (char*) malloc((size + 1) * sizeof (char));
    for (int i = 0; i < size; i++) {
        number[i] = t[i];
    }
    number[size] = '\0';
}

CBigInteger CBigInteger::operator%(const CBigInteger& rhs) {
    bool isNegative = (this->sign != rhs.sign);

    CBigInteger a(this->number, false);
    CBigInteger b(rhs.number, false);

    CBigInteger result;
    if (b == a) {
        result = *(new CBigInteger((char*)"0"));
    } else if (b > a) {
        result = a;
    } else {
        result = (a) - ((b) * (a / b));
        while (result.size > 0 && result.number[0] == '0') {
            for (int i = 0; i < result.size - 1; i++) {
                result.number[i] = result.number[i + 1];
            }
            result.size--;
            result.number[result.size] = '\0';
        }
    }
    result.sign = isNegative;

    return result;
}

CBigInteger operator*(const CBigInteger lhs, const CBigInteger & rhs) {
    CBigInteger zero((char*)"0");
    if (lhs == zero || rhs == zero) {
        return zero;
    }
    if (rhs.size > lhs.size) {
        CBigInteger a(lhs);
        CBigInteger b(rhs);
        return b*a;
    }

    CBigInteger a;
    a.size = lhs.size + rhs.size;
    if (lhs.sign == rhs.sign) {
        a.sign = false;
    } else {
        a.sign = true;
    }
    a.number = (char*) malloc((a.size + 1)* sizeof (char));
    for (int i = 0; i < a.size; i++)
        a.number[i] = '0';
    a.number[a.size] = '\0';

    int index1, index2, indexResult;
    indexResult = a.size - 1;
    index1 = lhs.size - 1;
    index2 = rhs.size - 1;

    char x1, x2, xrem = 0, xmul = 0, mrem = 0, m;
    int indent = 0;
    int indexRunner = lhs.size - 1;
    int lastPosition;
    while (index2 >= 0) {
        for (index1 = lhs.size - 1; index1 >= 0; index1--) {
            x1 = lhs.number[index1] - '0';
            x2 = rhs.number[index2] - '0';

            xmul = x1 * x2 + xrem; // + (a.number[a.size - 1 - indent - (indexRunner - index1)] - '0');
            xrem = xmul / 10;
            xmul %= 10;

            m = a.number[a.size - 1 - indent - (indexRunner - index1)] - '0';
            m += xmul + mrem;
            mrem = m / 10;
            m %= 10;
            a.number[a.size - 1 - indent - (indexRunner - index1)] = m + '0';

            lastPosition = a.size - 1 - indent - (indexRunner - index1);
        }
        index2--;
        indent++;
    }
    if (mrem > 0 || xrem > 0) {

        a.number[lastPosition - 1] = mrem + xrem + '0';
    }

    while ((a.size > 0)&&(a.number[0] == '0')) {
        for (int i = 0; i < a.size - 1; i++) {
            a.number[i] = a.number[i + 1];
        }
        a.size--;
    }

    return a;
}

CBigInteger operator/(const CBigInteger lhs, const CBigInteger & rhs) {
    CBigInteger zero((char*)"0");
    if ((rhs.sign == false)&&(lhs.sign == false)&&(rhs > lhs)) {
        return zero;
    }
    if ((rhs.sign == true)&&(lhs.sign == true)&&(rhs < lhs)) {
        return zero;
    }

    if (rhs == lhs) {
        if (rhs.sign == lhs.sign) {
            CBigInteger a((char*)"1");
            return a;
        }
        CBigInteger a((char*)"1", true);
        return a;
    }
    if (rhs == zero) {
        CBigInteger empty;
        return empty;
    }
    if (lhs == zero) {
        return zero;
    }
    CBigInteger a(lhs.number, false);
    CBigInteger b(rhs.number, false);

    bool isNegative = (lhs.sign != rhs.sign);

    while ((a.size > 0)&&(a.number[0] == '0')) {
        for (int i = 0; i < a.size - 1; i++) {
            a.number[i] = a.number[i + 1];
        }
        a.size--;
    }
    while ((b.size > 0)&&(b.number[0] == '0')) {
        for (int i = 0; i < b.size - 1; i++) {
            b.number[i] = b.number[i + 1];
        }
        b.size--;
    }

    //optimize division
    while ((a.size > 0)&&(b.size > 0)&&(a.number[a.size - 1] == '0')&&(b.number[b.size - 1] == '0')) {
        a.size--;
        a.number[a.size] = '\0';
        b.size--;
        b.number[b.size] = '\0';
    }

    char *result = (char*) malloc(a.size * sizeof (char));
    int indexResult = 0;

    char *t;
    int size = 0;
    int start = 0;

    CBigInteger c, cGood;
    CBigInteger one((char*)"1");
    CBigInteger ten((char*)"10");
    bool isBigEnough;

    while (a >= b) {
        isBigEnough = false;
        size = 0;
        for (int i = 0; i < a.size; i++) {
            if (a.number[i] != '0') {
                start = i;
                i = a.size;
            }
        }
        while (!isBigEnough) {
            size++;
            /*            if (size > 1) {
                            free(t);
                        }*/
            t = (char*) malloc((size + 1) * sizeof (char));
            for (int i = start; i < size; i++) {
                t[i] = a.number[i];
            }
            t[size] = '\0';

            c = CBigInteger(t);
            if (c >= b) {
                isBigEnough = true;
            }
        }

        CBigInteger test((char*)"1");
        CBigInteger mul = test*b;
        while (mul <= c) {
            test = test + one;
            mul = test*b;
        }
        test = test - one;

        for (int i = 0; i < test.size; i++) {
            result[indexResult] = test.number[i];
            indexResult++;
        }


        while (a >= c) {
            cGood = c;
            c = c * ten;
        }

        a = a - cGood;
    }
    
    CBigInteger resultDiv;

    resultDiv.size = indexResult;
    resultDiv.sign = isNegative;
    //    free(a.number);
    resultDiv.number = (char*) malloc((resultDiv.size + 1) * sizeof (char));
    for (int i = 0; i < resultDiv.size; i++) {
        resultDiv.number[i] = result[i];
    }
		resultDiv.number[resultDiv.size] = '\0';

    return resultDiv;
}

CBigInteger::~CBigInteger() {
	//cout << "about to destroy " << this << endl;
	delete[] number;
	number = NULL;//not strictly necessary
}