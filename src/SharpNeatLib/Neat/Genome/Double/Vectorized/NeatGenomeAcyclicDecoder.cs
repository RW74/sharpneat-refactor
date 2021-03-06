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
using System.Diagnostics;
using SharpNeat.BlackBox;
using SharpNeat.Evaluation;
using SharpNeat.Network.Acyclic;

namespace SharpNeat.Neat.Genome.Double.Vectorized
{
    /// <summary>
    /// For decoding instances of <see cref="NeatGenome{Double}"/> to <see cref="IBlackBox{Double}"/>, specifically 
    /// acyclic neural network instances implemented by <see cref="NeuralNet.Double.Vectorized.AcyclicNeuralNet"/>.
    /// </summary>
    public sealed class NeatGenomeAcyclicDecoder : IGenomeDecoder<NeatGenome<double>,IBlackBox<double>>
    {
        readonly bool _boundedOutput;

        #region Constructor

        /// <summary>
        /// Construct with the given decode arguments.
        /// </summary>
        /// <param name="boundedOutput">Indicates whether the output values at the output nodes should be bounded to the interval [0,1]</param>.
        public NeatGenomeAcyclicDecoder(bool boundedOutput)
        {
            _boundedOutput = boundedOutput;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Decode a genome into a working neural network.
        /// </summary>
        /// <param name="genome">The genome to decode.</param>
        public IBlackBox<double> Decode(
            NeatGenome<double> genome)
        {
            Debug.Assert(genome?.MetaNeatGenome?.IsAcyclic == true);
            Debug.Assert(null != genome?.ConnectionGenes);
            Debug.Assert(genome.ConnectionGenes.Length == genome?.ConnectionIndexMap?.Length);
            Debug.Assert(genome.DirectedGraph is AcyclicDirectedGraph);

            // Create neural net weight array.
            // Note. We cannot use the genome's weight array directly here (as is done in NeatGenomeDecoder,
            // i.e. for cyclic graphs) because the genome connections and digraph connections have a 
            // different order.
            double[] neuralNetWeightArr = Double.NeatGenomeAcyclicDecoder.CreateNeuralNetWeightArray(genome);

            // Create a working neural net.
            return new NeuralNet.Double.Vectorized.AcyclicNeuralNet(
                    (AcyclicDirectedGraph)genome.DirectedGraph,
                    neuralNetWeightArr,
                    genome.MetaNeatGenome.ActivationFn.Fn,
                    _boundedOutput);
        }

        #endregion
    }
}
