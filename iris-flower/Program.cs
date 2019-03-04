using System;
using Microsoft.ML;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Runtime.Learners;
using Microsoft.ML.Transforms.Conversions;

namespace net.robotmodel67.training.MLNet
{
    class IrisData 
    {
        public float SepalLength;
        public float SepalWidth;
        public float PetalLength;
        public float PetalWidth;
        public string Label;
    }

    class IrisPrediction 
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MLNet Iris type prediction");

            var mlContext = new MLContext();
            string dataPath = "assets/iris.data";
            var reader = mlContext.Data.TextReader(
                new TextLoader.Arguments() {
                    Separator = ",",
                    HasHeader = true,
                    Column = new[] {
                        new TextLoader.Column("SepalLength", DataKind.R4, 0),
                        new TextLoader.Column("SepalWidth", DataKind.R4, 1),
                        new TextLoader.Column("PetalLength", DataKind.R4, 2),
                        new TextLoader.Column("PetalWidth", DataKind.R4, 3),
                        new TextLoader.Column("Label", DataKind.Text, 4),

                    }

                }
            );

            IDataView trainingDataView = reader.Read(
                new MultiFileSource(dataPath)
            );

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Concatenate("Features","SepalLength","SepalWidth","PetalLength","PetalWidth"))
                .Append(mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(labelColumn: "Label", featureColumn: "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(trainingDataView);

            var prediction = model.MakePredictionFunction<IrisData, IrisPrediction>(mlContext).Predict(
                new IrisData()
                {
                    SepalLength = 3.3f,
                    SepalWidth = 1.6f,
                    PetalLength = 0.4f,
                    PetalWidth = 5.1f,
                }
            );

            Console.WriteLine($"Predicted flower type is: {prediction.PredictedLabels}");

        }
    }
}
