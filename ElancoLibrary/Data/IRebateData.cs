﻿using ElancoLibrary.Models;

namespace ElancoLibrary.Data
{
    public interface IRebateData
    {
        Task SubmitRebate(FormModel form);
    }
}