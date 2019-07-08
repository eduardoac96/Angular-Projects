using System;
using System.Collections.Generic;
using System.Text;

namespace LearnU_DomainEntities
{
    public class ActionResponseDTO
    {
        public bool IsSuccessful { get; }
        public string FriendlyErrorMessage { get; }
        public ActionResponseDTO()
        {
            this.IsSuccessful = true;
        }
        public ActionResponseDTO(string friendlyErrorMessage, bool isSuccessful = false)
        {
            this.IsSuccessful = isSuccessful;
            this.FriendlyErrorMessage = friendlyErrorMessage;
        }
    }
}
