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
using Novell.Directory.Ldap;

namespace ZENReports
{
	public static class AttributeUtil
	{
		/// <summary>
		/// Returns the attribute value specified in the second string parameter
		/// 
		/// TODO: Need more error handling 
		/// </summary>
		/// <param name="entry">
		/// A <see cref="LdapEntry"/>
		/// </param>
		/// <param name="attr">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public static string getAttr(LdapAttributeSet attrSet, string attr) {
			Logger.Debug("Requesting Attribute value of {0}", attrSet.getAttribute(attr));
			if (attrSet.getAttribute(attr) == null)
				return null;
			else {
				Logger.Debug(" Attribute {0} -> {1}", attr, attrSet.getAttribute(attr).StringValue);			
				return attrSet.getAttribute(attr).StringValue;
			}
		}	
		/// <summary>
		/// Parses a LdapAttributeSet and the specified user DN
		/// Returns a user object.
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="dn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="LDAPUser"/>
		/// </returns>
		public static LDAPUser iterUsrAttrs(LdapAttributeSet attrSet, string dn) {
			LDAPUser user;
			System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
			
			if (attrSet.Count == 0) {
				Logger.Debug("No attributes in the AttributeSet for {0}", dn);
				return null;
			}
			
			user = new LDAPUser(dn);
			
			while(ienum.MoveNext())
			{				
				
				LdapAttribute attribute=(LdapAttribute)ienum.Current;		
				Logger.Debug("Parsing {0}", attribute);
				
				if (attribute.Name.ToUpper().Equals(ATTRNAME.NDSHOMEDIRECTORY.ToString()))
					user.parseNdsHomeDirPath(AttributeUtil.getAttr(attrSet, ATTRNAME.NDSHOMEDIRECTORY.ToString()));	
				
				if (attribute.Name.ToUpper().Equals(ATTRNAME.SN.ToString()))
					user.setSN(AttributeUtil.getAttr(attrSet, ATTRNAME.SN.ToString()));
								
				if (attribute.Name.ToUpper().Equals(ATTRNAME.GIVENNAME.ToString()))
					user.setGivenName(AttributeUtil.getAttr(attrSet, ATTRNAME.GIVENNAME.ToString()));
				
			}
			return user;	
		}		
		/// <summary>
		/// Parses a group objects attributes building the LDAPGroup object
		/// Requires the group objects attribute set and the DN.
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="dn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="LDAPUser"/>
		/// </returns>
		public static LDAPGroup iterGroupAttrs(LdapAttributeSet attrSet, string dn) {
			LDAPGroup grp;
			System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
			
			if (attrSet.Count == 0)
				return null;
			
			grp = new LDAPGroup(dn);
			
			while(ienum.MoveNext())
			{				
				LdapAttribute attribute=(LdapAttribute)ienum.Current;			
				if (attribute.Name.ToUpper().Equals(ATTRNAME.SN.ToString()))
					grp.setSN(AttributeUtil.getAttr(attrSet, ATTRNAME.SN.ToString()));
				
				if (attribute.Name.ToUpper().Equals(ATTRNAME.CN.ToString()))
					grp.setCN(AttributeUtil.getAttr(attrSet, ATTRNAME.CN.ToString()));				
				
				if (attribute.Name.ToUpper().Equals(ATTRNAME.MEMBERS.ToString()))
					grp.setGroupMembers( AttributeUtil.getListofAttr(attrSet, ATTRNAME.MEMBERS.ToString()));
			}
			return grp;
		}
		
		/// <summary>
		/// Iterates the ZFD Application attributes
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="dn">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="LDAPGroup"/>
		/// </returns>
		public static LDAPZFDApp iterZFDAppAttrs(LdapAttributeSet attrSet, string dn) {
			LDAPZFDApp app;
			System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
			
			if (attrSet.Count == 0)
				return null;
			
			app = new LDAPZFDApp(dn);
			
			while(ienum.MoveNext())
			{				
				LdapAttribute attribute=(LdapAttribute)ienum.Current;			
				if (attribute.Name.ToUpper().Equals(ATTRNAME.APPASSOCIATIONS))
					app.setAssociations(AttributeUtil.getListofAttr(attrSet, ATTRNAME.APPASSOCIATIONS.ToString()));
				
			}
			return app;
		}
		/// <summary>
		/// Returns null of no attributes match the attr parameter value
		/// Returns a list of strings that contain the attribute values that were specified in the attr param
		/// </summary>
		/// <param name="attrSet">
		/// A <see cref="LdapAttributeSet"/>
		/// </param>
		/// <param name="attr">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="List<System.String>"/>
		/// </returns>
		public static List<string> getListofAttr(LdapAttributeSet attrSet, string attr) {
			
			if (attrSet.getAttribute(attr) == null)
				return null;
			else {
				List<string> values = null;
				System.Collections.IEnumerator ienum =  attrSet.GetEnumerator();
				
				while(ienum.MoveNext())
				{
					LdapAttribute attribute=(LdapAttribute)ienum.Current;
					if (attribute.Name.ToUpper().Equals(attr)) {
						values = new List<string>(attrSet.getAttribute(attr).StringValueArray.Length);
						values.AddRange(attrSet.getAttribute(attr).StringValueArray); // take the values from the array
						
						if (Logger.LogLevel == Level.DEBUG) {
							foreach (string x in values) //debug purposes
							Logger.Debug("Values in {0} list {1}", attr, x);
						}
					}
				}
			return values;
			}
		}		
	}
}
