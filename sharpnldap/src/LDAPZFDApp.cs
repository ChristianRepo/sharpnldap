/******************************************************************************
* The MIT License
* Copyright (c) 2010 Jared L Jennings
* 
* Permission is hereby granted, free of charge, to any person obtaining  a copy
* of this software and associated documentation files (the Software), to deal
* in the Software without restriction, including  without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
* copies of the Software, and to  permit persons to whom the Software is 
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in 
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*******************************************************************************/
//
//
// Author:
//   Jared L Jennings (jaredljennings@gmail.com)
//
// (C) 2010 Jared L Jennings (jaredljennings@gmail.com)
//
using System;
using System.Collections.Generic;

namespace sharpnldap
{


	public class LDAPZFDApp : LDAPObject
	{
		private List<string> assocations;
		
		public LDAPZFDApp (string x) {
			dn = x;
		}
		
		public void setAssociations(string[] vals) {
			List<string> valsList = new List<string>(vals.Length);	
			assocations = new List<string>();
			assocations.AddRange(valsList);
		}
		
		public void setAssociations(List<string> vals) {
			assocations = vals;
		}
		
		public List<string> getAssociations() {
			if (this.assocations != null)
				return this.assocations;
			else
				return null;
		}
		
	}
}