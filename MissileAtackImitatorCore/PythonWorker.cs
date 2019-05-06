//using System;
//using System.Collections.Generic;

//namespace MissileAtackImitatorCoreNS
//{
//    public class PythonWorker
//    {
//        public void Init()
//        {
//            try
//            {
//                //var engine = Python.CreateEngine();
//                //var source = engine.CreateScriptSourceFromFile("test.py");
//                //var scope = engine.CreateScope();
//                //source.Execute(scope);

//                //dynamic Calculator = scope.GetVariable("Calculator");
//                //dynamic calc = Calculator();
//                //int result = calc.add(4, 5);

//                //var xValues = new List<int>();
//                //xValues.Add(0);
//                //xValues.Add(1);
//                //xValues.Add(2);

//                //var yValues = new List<int>();
//                //yValues.Add(3);
//                //yValues.Add(4);
//                //yValues.Add(5);

//                //var engine = Python.CreateEngine();
//                //var scope = engine.CreateScope();
//                //engine.ExecuteFile("aircrafttrajectorygenerator.py", scope);
//                ////var source = engine.CreateScriptSourceFromFile("aircrafttrajectorygenerator.py");
//                //dynamic function = scope.GetVariable("buildTrajectory");
//                //var result = function(xValues, yValues);

//                using (Py.GIL())
//                {
//                    dynamic np = Py.Import("numpy");
//                    Console.WriteLine(np.cos(np.pi * 2));

//                    dynamic sin = np.sin;
//                    Console.WriteLine(sin(5));

//                    double c = np.cos(5) + sin(5);
//                    Console.WriteLine(c);

//                    dynamic a = np.array(new List<float> { 1, 2, 3 });
//                    Console.WriteLine(a.dtype);

//                    dynamic b = np.array(new List<float> { 6, 5, 4 }, dtype: np.int32);
//                    Console.WriteLine(b.dtype);

//                    Console.WriteLine(a * b);
//                    Console.ReadKey();
//                }

//            }
//            catch (Exception ex)
//            {
//                string error = ex.Message;

//                while (ex.InnerException.Message != null)
//                {
//                    error += Environment.NewLine + ex.InnerException.Message;
//                }
//            }
//        }
//    }
//}
