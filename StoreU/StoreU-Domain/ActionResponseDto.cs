using System;
using System.Collections.Generic;
using System.Text;

namespace StoreU_DomainEntities
{
    public class ActionResponseDto
    {
        public bool IsSuccessful { get; }
        public string FriendlyErrorMessage { get; }
        public ActionResponseDto()
        {
            this.IsSuccessful = true;
        }
        public ActionResponseDto(string friendlyErrorMessage, bool isSuccessful = false)
        {
            this.IsSuccessful = isSuccessful;
            this.FriendlyErrorMessage = friendlyErrorMessage;
        }
    }
}
