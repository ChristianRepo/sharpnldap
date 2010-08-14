using System;
using System.Collections.Generic;
using NUnit.Framework;
namespace sharpnldap
{
	[TestFixture()]
	public class testLDAPUser
	{
		[Test()]
		public void TestCreateUser ()
		{
			LDAPUser user = new LDAPUser("cn=user1,ou=users,o=kc");
			user.setGivenName("user1");
			user.setSN("jennings");
			List<string> members = new List<string>();
			members.Add("cn=everyone,ou=groups,o=kc");
			user.setGroupMemberOf(members);
			Assert.AreSame("cn=user1,ou=users,o=kc", user.getDN());
			Assert.AreSame("user1", user.getGivenName());
			Assert.AreSame("jennings", user.getSN());
			Assert.IsTrue(user.getGroupMemberOf().Contains("cn=everyone,ou=groups,o=kc"));
			Assert.AreSame("person", user.OBJECTCLASS);
		}
	}
}

