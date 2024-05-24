using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKAT
{
    public class Shannon_Fano
    {
        private const int bytes = 256;

        public Shannon_Fano() { }

        private double[] arrayBytes()
        {
            double[] array = new double[bytes];
            for (int i = 0; i < bytes; i++)
            {
                array[i] = Convert.ToDouble(i);
            }

            return array;
        }

        private void sort(ref int[] array, ref double[] values)
        {
            int i, j;
            int imax;

            for (i = 0; i < bytes - 1; i++)
            {
                imax = i;

                for (j = i + 1; j < bytes; j++)
                {
                    if (array[j] > array[imax])
                        imax = j;
                }

                if (i != imax)
                {
                    int pom = array[i];
                    array[i] = array[imax];
                    array[imax] = pom;

                    double p = values[i];
                    values[i] = values[imax];
                    values[imax] = p;
                }
            }
        }

        private void findPositions(out int start, out int end, int[] array)
        {
            start = 0;
            end = 1;

            int part1 = array[0];
            int part2 = 0;

            for (int i = 0; i < array.Count(); i++)
            {
                part2 += array[i];
            }
            int minDiff = Math.Abs(part1 - part2);

            for (int i = 1; i < array.Count() - 1; i++)
            {
                part1 += array[i];
                part2 -= array[i];
                if (Math.Abs(part1 - part2) < minDiff)
                {
                    minDiff = Math.Abs(part1 - part2);
                    end = i + 1;
                }
            }
        }

        private BTree createTree(int start, int end, int[] array, double[] values, int difference, StreamWriter sw)
        {
            BTree tree = new BTree();
            BNode left = null;
            BNode right = null;

            tree.root = new BNode();
            if (difference == 1)
            {
                tree.root = new BNode(values[start]);
                return tree;
            }

            if (difference == 2)
            {
                tree.root.left = new BNode(values[start]);
                tree.root.right = new BNode(values[end], 1);
                return tree;
            }
            tree.root.left = new BNode();
            tree.root.right = new BNode();
            tree.root.right.weight = 1;

            left = tree.root.left;

            if (Math.Abs(start - end) == 1)
            {
                left.left = new BNode(array[start]);
            }
            else
            {
                for (int i = start; i < end - 1; i++)
                {

                    left.left = new BNode(values[i]);

                    if ((i + 1) == (end - 1))
                    {
                        left.right = new BNode(values[i + 1], 1);
                    }
                    else
                    {
                        left.right = new BNode(1);
                        left = left.right;
                    }
                }
            }

            right = tree.root.right;
            if (Math.Abs(difference - end) == 1)
            {
                right.left = new BNode(array[end]);
            }
            else
            {
                for (int i = end; i < difference - 1; i++)
                {

                    right.left = new BNode(values[i]);

                    if ((i + 1) == (difference - 1))
                    {
                        right.right = new BNode(values[i + 1], 1);
                    }
                    else
                    {
                        right.right = new BNode(1);
                        right = right.right;
                    }
                }
            }

            int[] arr = new int[difference];
            printCodes(tree.root, arr, 0, sw);
            return tree;
        }

        private void printCodes(BNode root, int[] array, int top, StreamWriter sw)
        {
            if (root.left != null)
            {
                array[top] = 0;
                printCodes(root.left, array, top + 1, sw);
            }

            if (root.right != null)
            {
                array[top] = 1;
                printCodes(root.right, array, top + 1, sw);
            }
            if (root.left == null && root.right == null)
            {
                sw.Write(root.info + " | ");
                int i;
                for (i = 0; i < top; ++i)
                    sw.Write(array[i]);

                sw.WriteLine();
            }
        }

        public void identifyDifferents(int[] array, double[] values, StreamWriter sw)
        {
            int difference = 0;

            double[] val = arrayBytes();

            foreach (double b in values)
            {
                double pom = Math.Abs(b);
                if (array[Convert.ToInt32(pom)] == 0)
                    difference++;
                array[Convert.ToInt32(pom)]++;
            }

            sort(ref array, ref val);

            int[] frequency = new int[difference];
            double[] values2 = new double[difference];

            for (int i = 0; i < difference; i++)
            {
                frequency[i] = array[i];
                values2[i] = val[i];
            }

            array = frequency;
            val = values2;

            int start, end;
            findPositions(out start, out end, array);

            BTree tree = createTree(start, end, array, val, difference, sw);
        }
    }
}
