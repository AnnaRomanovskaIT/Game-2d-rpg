using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryOperation
{
    Add,
    Remove
}

public class InventoryException : Exception
{
    public InventoryOperation Operation { get; }
    public InventoryException(InventoryOperation operation, string msg) : base($"{operation} Error: {msg}")
    {
        Operation = operation;

    }
}
