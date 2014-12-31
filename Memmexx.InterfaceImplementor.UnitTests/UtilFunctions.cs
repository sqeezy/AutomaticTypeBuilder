/*
 * Interface Object Factory
 * 
 * Automatically generates objects that implement a given interface without the need to provide a 
 * pre-existing class
 * 
 * (c) 2007 Andrew Rondeau
 * http://andrewrondeau.com
 * 
 * Permission is granted to use this program freely in any computer program provided that this 
 * notice is not removed from the source code.  Modified versions of this source code may be 
 * published provided that this notice is altered 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Memmexx.InterfaceImplementor.UnitTests
{
    public delegate void MyDelegate();

    static class UtilFunctions
    {
        public static void SendExceptionToConsole(MyDelegate del)
        {
            try
            {
                del();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
