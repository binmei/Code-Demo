/**
 *CSCD 320 Algorithm Programming Project - Source Code
 *By: Bin Mei
 *May 17th, 2014
 **/

import java.util.Arrays; //Prof. Xu said it's okay to import util.Arrays
import java.util.Scanner;
import java.io.*;


////////////////////////////////////////////////////////////////////////////////////
//
//Main
//
////////////////////////////////////////////////////////////////////////////////////
public class FindTenKLargest {

	public static void main(String[] args) {
	   File file = new File("data.txt");
      MinHeap heap = new MinHeap(10000);
      int count = 0;
		try {
			Scanner fin = new Scanner(file);
			while (fin.hasNext() ) {
				int num = fin.nextInt();
   			if (count < 10000){
   				heap.insert(num);
   			}
   			else
   			{
   				heap.insertAtRoot(num);
   			}	
            fin.nextLine();
				count++;
			}
         System.out.println(Arrays.asList(heap.getData()));
			fin.close();
		} catch (Exception e) {
 			System.out.println("You messed up, file no read!");
 		}
	}
}


////////////////////////////////////////////////////////////////////////////////////
//
//MinHeap Object
//
////////////////////////////////////////////////////////////////////////////////////

class MinHeap {

	private Integer[] data;
	private int heapSize;

	public MinHeap(int size) {
		data = new Integer[size];
		heapSize = 0;
	}
   
	public Integer[] getData(){
		return data;
	}

	public boolean isEmpty() {
		return (heapSize == 0);
	}

	private int getLeftIndex(int nodeIndex) {
		return 2 * nodeIndex + 1;
	}

	private int getRightIndex(int nodeIndex) {
		return 2 * nodeIndex + 2;
	}

	private int getParentIndex(int nodeIndex) {
		return (nodeIndex - 1) / 2;
	}

	public int getMinimum() {
		if (isEmpty())
			throw new HeapException("Heap is empty");
		else
			return data[0];
	}
   
	class HeapException extends RuntimeException {
		private static final long serialVersionUID = 1L;
		public HeapException(String message) {
			super(message);
		}
	}

	public void insert(int value) {
		if (heapSize == data.length)
			throw new HeapException("Heap's underlying storage is overflow");
		else {
			heapSize++;
			data[heapSize - 1] = value;
			shiftUp(heapSize - 1);
		}
	}

	private void shiftUp(int nodeIndex) {
		int parentIndex, tmp;
		if (nodeIndex != 0) {
			parentIndex = getParentIndex(nodeIndex);
			if (data[parentIndex] > data[nodeIndex]) {
				tmp = data[parentIndex];
				data[parentIndex] = data[nodeIndex];
				data[nodeIndex] = tmp;
				shiftUp(parentIndex);
			}
		}
	}

	public void removeMin() {
		if (isEmpty())
			throw new HeapException("The heap is empty!");
		else {
			data[0] = data[heapSize - 1];
			data[heapSize-1]=null;
			heapSize--;
			if (heapSize > 0)
				shiftDown(0);
		}
	}
	
	public void insertAtRoot(int item){
		if (isEmpty())
			throw new HeapException("The heap is empty!");
		else if (data[0] < item){
			data[0] = item;
			shiftDown(0);
		}
	}
   
	private void shiftDown(int nodeIndex) {
		int leftChildIndex, rightChildIndex, minIndex, tmp;
		leftChildIndex = getLeftIndex(nodeIndex);
		rightChildIndex = getRightIndex(nodeIndex);
		if (rightChildIndex >= heapSize) {
			if (leftChildIndex >= heapSize)
				return;
			else
				minIndex = leftChildIndex;
		} else {
			if (data[leftChildIndex] <= data[rightChildIndex])
				minIndex = leftChildIndex;
			else
				minIndex = rightChildIndex;
		}
		if (data[nodeIndex] > data[minIndex]) {
			tmp = data[minIndex];
			data[minIndex] = data[nodeIndex];
			data[nodeIndex] = tmp;
			shiftDown(minIndex);
		}
	}
}