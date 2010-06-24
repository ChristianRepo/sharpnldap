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
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ZENReports
{

	/// <summary>
	/// Represents a user object in eDirectory
	/// </summary>
	public class LDAPUser : LDAPObject
	{
		public LDAPUser (string x) {
			dn = x;
		}
		private List<string> memberOf;
		/// <summary>
		/// Contains the value which is read directly from eDirectory
		/// </summary>
		private string _ndsHomeDirectory;
		private string _ndsHomeVol;
		private string _ndsHomePath;
		private string _ndsHomeServer;
		
		/// <summary>
		/// Parses the eDirectory attribute ndsHomeDirectory which contains the 
		/// path, volume and server where the users home folder resides.
		/// 
		/// The parsed information is set to the different methods, i.e. home Vol, Home Path, Home Server
		/// the is set to null if a null parameter is passed.
		/// </summary>
		/// <param name="s">
		/// A <see cref="System.String"/>
		/// </param>
		public void parseNdsHomeDirPath(string s) {
			
			if ((s == null) || (s.Length == 0)) {
				Logger.Debug("Passed a null or empty ndshomedirectorypath");
				_ndsHomeDirectory = null;
			}
			else {
				string[] a = Regex.Split(s, @",");
				Logger.Debug ("Split NdsHomeDirPath {0}", a[0]);
				
				
				string b = stripFQN(a[0]); // remove the cn=
				string[] c = Regex.Split(b, @"_"); // remove the volume from the server
				
				if (c[0] != null) { // get the server from the string
					Logger.Debug("ndsHomeServer {0}", c[0]);
					_ndsHomeServer = c[0];
				}
				if (c[1] != null) { // get volume from string
					Logger.Debug("ndsHomeVol {0}", c[1]);
					_ndsHomeVol = c[1];
				}
				
				/* get folder and path from string.
				 * TODO: Really sloppy. should just get last of the string values
				 */
				string p = s.SubstringAfter("#").SubstringAfter("#");
				Logger.Debug("Sub after {0}", p);
				_ndsHomePath = p;
				
				_ndsHomeDirectory = s;
			}
			
		}
		
		/// <summary>
		/// Returns the UNC path of the users home directory.
		/// 
		/// returns null if the server, volume and path are not pulated.
		/// Basically if the parseNdsHomeDirectory method wasn't called, these values will be empty.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getUncHome () {
			if ((_ndsHomeServer == null) | (_ndsHomeVol == null) | (_ndsHomePath == null))
				return null;
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append( @"\\");
			sb.Append(_ndsHomeServer);
			sb.Append(@"\");
			sb.Append(_ndsHomeVol);
			sb.Append(@"\");
			sb.Append(_ndsHomePath);
			return sb.ToString();
		}

		/// <summary>
		/// Returns the actual ndsHomeDirectory attribute value that was associated with the user.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getNdsHomeDirectory() {
			return _ndsHomeDirectory;
		}
		
		/// <summary>
		/// Strings the cn= or whatever object type that is normally specified in FQN LDAP syntax strings
		/// returns the string of whatever was passed after the = character
		/// </summary>
		/// <param name="s">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		private string stripFQN(string s) {
			string[] a = Regex.Split(s, @"=");
			Logger.Debug("stripFQN {0}", a[1]);
			return a[1];
		}
		
		/// <summary>
		/// Returns the path of the users home directory. This does include the users home folder name
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getNdsHomePath() {
			return _ndsHomePath;
		}
		
		/// <summary>
		/// Returns the users volume that holds the users home directory
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getNdsHomeVol() {
			return _ndsHomeVol;
		}
		
		/// <summary>
		/// Returns the server hold the users home directory volume
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string getNdsHomeServer() {
			return _ndsHomeServer;
		}
		
		/// <summary>
		/// Sets what group objects the user is a member of 
		/// This clears any preexisting group members from the list
		/// </summary>
		/// <param name="mbrs">
		/// A <see cref="List<System.String>"/>
		/// </param>
		public void setGroupMemberOf(List<string> mbrs) {
			this.memberOf = mbrs;
		}
		/// <summary>
		/// Adds a group object to the list of group memberships
		/// </summary>
		/// <param name="mbr">
		/// A <see cref="System.String"/>
		/// </param>
		public void addGroupMemberOf(string mbr) {
			if (this.memberOf == null)
				memberOf = new List<string>();
			else
				this.memberOf.Add (mbr);
		}
		/// <summary>
		/// Returns a list of group names that the user is a member of
		/// </summary>
		/// <returns>
		/// A <see cref="List<System.String>"/>
		/// </returns>
		public List<string> getGroupMemberOf() {
			return this.memberOf;
		}
		
	}
}
