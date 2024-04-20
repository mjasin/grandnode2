﻿using Grand.Infrastructure.Models;

namespace Grand.Web.Common.Models;

public class DeleteConfirmationModel : BaseEntityModel
{
    public string ControllerName { get; set; }
    public string ActionName { get; set; }
    public string WindowId { get; set; }
}