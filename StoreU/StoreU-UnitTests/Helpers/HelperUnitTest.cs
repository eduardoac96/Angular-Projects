using StoreU_WepApi.Helpers;
using StoreU_WebApi.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreU_UnitTests.Helpers
{
    public class HelperUnitTest
    {
        public HelperUnitTest()
        {

        }


        [Fact]
        public void EncryptToken()
        {
            try
            {
                string userToken = "40CB99E6-0C31-4FF7-996A-6126C086FAD3";
                string encryptedToken = EncryptionHelper.Encrypt(userToken, Constants.TOKEN_KEY);

                Assert.True(true, "Success");
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }

        [Fact]
        public void DecryptToken()
        {
            try
            {
                string encryptedToken = "ON1bOuv7e1LhgxTLIRlODfWNXYdavVaL4a/dMprzLnkrthyGBtYVUhB4C3D/fevSvbksbMj6XcpOMEeqqAMJqw==";

                string decryptedToken = EncryptionHelper.Decrypt(encryptedToken, Constants.TOKEN_KEY);

                Assert.True(true, "Success");
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
                
            }
        }
    }
}
