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

namespace ZENReports
{
	/// <summary>
	/// This object class acts as the admin user for the whole package
	/// This class contains all the needed information that is required for a
	/// ldap connection to eDirectory
	/// 
	/// The class is sealed, a singleton and thread safe
	/// </summary>
	public class AuthUser
	{
		private string username;
		private string password;
		private string ldaphost;
		private int ldapport;
		private string basedn;
		private int ldaps;
		private bool secureLDAP;
		
		public void setSecureLDAP(bool b) {
			secureLDAP = b;
		}
		
		public bool getSecureLDAP() {
			return secureLDAP;
		}
		
		public int getLDAPSPort() {
			return ldaps;
		}
		
		public void setLDAPSPort(int s) {
			ldaps = s;
		}
		
		public string getBaseDN() {
			return basedn;
		}
		
		public void setBaseDN(string dn) {
			basedn = dn;
		}
		
		public void setUsername(string u) {
			username = u;
		}
		
		public string getUsername() {
			return username;
		}
		public void setPassword(string p) {
			password = p;
		}
		
		public string getPassword() {
			return password;
		}
		
		public void setLDAPhost(string l) {
			ldaphost = l;
		}
		
		public void setLDAPport(int p) {
			ldapport = p;
		}
		
		public int getLDAPport() {
			return ldapport;
		}
		
		public string getLDAPhost() {
			return ldaphost;
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		public AuthUser ()
		{

		}
	}
}