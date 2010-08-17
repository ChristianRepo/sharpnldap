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
			user.parseNdsHomeDirPath(@"cn=SOUTHSTUDENT_GENERAL,ou=ACHS,o=USD385#0#Home\2013\WildMC");
			Assert.AreEqual("cn=user1,ou=users,o=kc", user.getDN());
			Assert.AreEqual("user1", user.getGivenName());
			Assert.AreEqual("jennings", user.getSN());
			Assert.IsTrue(user.getGroupMemberOf().Contains("cn=everyone,ou=groups,o=kc"));
			Assert.AreEqual("person", user.OBJECTCLASS);
			Assert.AreEqual(@"Home\2013\WildMC", user.ndsHomePath);
			Assert.AreEqual("SOUTHSTUDENT", user.ndsHomeServer);
			Assert.AreEqual("GENERAL", user.ndsHomeVolume);
		}
	}
}

