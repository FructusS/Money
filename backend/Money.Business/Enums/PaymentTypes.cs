﻿using Money.Common;

namespace Money.Business.Enums;

public class OperationTypes(int value, string name, string description) : Enumeration(value, name)
{
    /// <summary>
    /// Расходы - 1.
    /// </summary>
    public static readonly OperationTypes Costs = new(1, nameof(Costs), "Расходы");

    /// <summary>
    /// Доходы - 2.
    /// </summary>
    public static readonly OperationTypes Income = new(2, nameof(Income), "Доходы");

    public string Description { get; private set; } = description;

    public static implicit operator OperationTypes(int value)
    {
        return FromValue<OperationTypes>(value);
    }

    public static implicit operator OperationTypes(string name)
    {
        return FromName<OperationTypes>(name);
    }
}
