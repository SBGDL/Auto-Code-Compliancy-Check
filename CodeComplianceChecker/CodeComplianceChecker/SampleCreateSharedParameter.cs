﻿using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using System;

namespace CodeComplianceChecker
{
    internal class SampleCreateSharedParameter
    {
        public void CreateSampleSharedParameters(Document doc, Application app)
        {
            Category category = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
            CategorySet categorySet = app.Create.NewCategorySet();
            categorySet.Insert(category);

            string originalFile = app.SharedParametersFilename;
            // for company standard shared parameter location
            string tempFile = @"C:\Users\FESG\Desktop\Soheila\BIM\C# programmming\Shared Parameters\New Parameters.txt";

            try
            {
                app.SharedParametersFilename = tempFile;

                DefinitionFile sharedParameterFile = app.OpenSharedParameterFile();

                foreach (DefinitionGroup dg in sharedParameterFile.Groups)
                {
                    if (dg.Name == "My New Parameters")
                    {
                        ExternalDefinition externalDefinition = dg.Definitions.get_Item("GROUP 1") as ExternalDefinition;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameters");
                            //parameter binding 
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);
                            //parameter group to text
                            doc.ParameterBindings.Insert(externalDefinition, newIB, BuiltInParameterGroup.PG_TEXT);
                            t.Commit();
                        }
                    }
                }
            }
            catch { }
            finally
            {
                //reset to original file
                app.SharedParametersFilename = originalFile;
            }
        }
    }
}
