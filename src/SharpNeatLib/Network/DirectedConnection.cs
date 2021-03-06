﻿/* ***************************************************************************
 * This file is part of SharpNEAT - Evolution of Neural Networks.
 * 
 * Copyright 2004-2019 Colin Green (sharpneat@gmail.com)
 *
 * SharpNEAT is free software; you can redistribute it and/or modify
 * it under the terms of The MIT License (MIT).
 *
 * You should have received a copy of the MIT License
 * along with SharpNEAT; if not, see https://opensource.org/licenses/MIT.
 */
using System;

namespace SharpNeat.Network
{
    /// <summary>
    /// Represents a connection between two nodes. Used primarily as a key into a Dictionary that 
    /// uniquely identifies connections by their end points.
    /// </summary>
    public readonly struct DirectedConnection : IEquatable<DirectedConnection>, IComparable<DirectedConnection>
    {
        #region Auto Properties

        /// <summary>
        /// Connection source node ID.
        /// </summary>
        public int SourceId { get; }
        /// <summary>
        /// Connection target node ID.
        /// </summary>
        public int TargetId { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construct with the provided source and target node IDs.
        /// </summary>
        public DirectedConnection(int srcId, int tgtId)
        {
            this.SourceId = srcId;
            this.TargetId = tgtId;
        }

        /// <summary>
        /// Construct with the provided source and target node IDs.
        /// </summary>
        public DirectedConnection(in DirectedConnection copyFrom)
        {
            this.SourceId = copyFrom.SourceId;
            this.TargetId = copyFrom.TargetId;
        }

        #endregion

        #region IEquatable / IComparable

        /// <summary>
        /// Determines whether the specified <see cref="DirectedConnection" /> is equal to the current <see cref="DirectedConnection" />.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the objects are equal; otherwise false.</returns>
        public bool Equals(DirectedConnection other)
        {
            return (this.SourceId == other.SourceId) 
                && (this.TargetId == other.TargetId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DirectedConnection other)
        {
            int v = this.SourceId.CompareTo(other.SourceId);
            if(v != 0) return v;

            return this.TargetId.CompareTo(other.TargetId);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="DirectedConnection" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the objects are equal; otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if(obj is DirectedConnection) {
                return this.Equals((DirectedConnection)obj);
            }
            return false;
        }

        /// <summary>
        /// Get the hash code for the current object.
        /// </summary>
        /// <returns>The current object's hash code.</returns>
        public override int GetHashCode()
        {
            // Variant on FNV hash taken from: http://stackoverflow.com/a/263416/15703
            unchecked
            {
                int v = (int)2166136261;
                v = (v * 16777619) ^ SourceId;
                v = (v * 16777619) ^ TargetId;
                return v;
            }
        }

        /// <summary>
        /// Determines whether two <see cref="DirectedConnection"/>s have the same value.
        /// </summary>
        /// <param name="x">The first <see cref="DirectedConnection"/> to compare.</param>
        /// <param name="y">The second <see cref="DirectedConnection"/> to compare.</param>
        /// <returns>true if the two <see cref="DirectedConnection"/>s are equal; otherwise false.</returns>
        public static bool operator ==(in DirectedConnection x, in DirectedConnection y)
        {
            return (x.SourceId == y.SourceId) 
                && (x.TargetId == y.TargetId);
        }

        /// <summary>
        /// Determines whether two <see cref="DirectedConnection"/>s have a different value.
        /// </summary>
        /// <param name="x">The first <see cref="DirectedConnection"/> to compare.</param>
        /// <param name="y">The second <see cref="DirectedConnection"/> to compare.</param>
        /// <returns>true if the two <see cref="DirectedConnection"/>s are different; otherwise false.</returns>
        public static bool operator !=(in DirectedConnection x, in DirectedConnection y)
        {
            return (x.SourceId != y.SourceId) 
                || (x.TargetId != y.TargetId);
        }

        /// <summary>
        /// Determines whether a specified <see cref="DirectedConnection"/> is less than another specified <see cref="DirectedConnection"/>.
        /// </summary>
        /// <param name="x">The first <see cref="DirectedConnection"/> to compare.</param>
        /// <param name="y">The second <see cref="DirectedConnection"/> to compare.</param>
        /// <returns>true if <paramref name="x" /> is less than <paramref name="y" />; otherwise, false.</returns>
        public static bool operator <(in DirectedConnection x, in DirectedConnection y)
        {
            return x.CompareTo(y) < 0;
        }

        /// <summary>
        /// Determines whether a specified <see cref="DirectedConnection"/> is greater than another specified <see cref="DirectedConnection"/>.
        /// </summary>
        /// <param name="x">The first <see cref="DirectedConnection"/> to compare.</param>
        /// <param name="y">The second <see cref="DirectedConnection"/> to compare.</param>
        /// <returns>true if <paramref name="x" /> is greater than <paramref name="y" />; otherwise, false.</returns>
        public static bool operator >(in DirectedConnection x, in DirectedConnection y)
        {
            return x.CompareTo(y) > 0;
        }

        #endregion
    }
}
