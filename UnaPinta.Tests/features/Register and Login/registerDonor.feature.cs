// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace UnaPinta.Tests.Features.RegisterAndLogin
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class RegistroDeDonanteFeature : object, Xunit.IClassFixture<RegistroDeDonanteFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "registerDonor.feature"
#line hidden
        
        public RegistroDeDonanteFeature(RegistroDeDonanteFeature.FixtureData fixtureData, UnaPinta_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "features/Register and Login", "Registro de donante", "\tComo nuevo usuario, Quiero registrarme como donante ingresando mis datos,\r\n\tPara" +
                    " que el sistema me recuerde", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Registrando un donante satisfactoriamente")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro de donante")]
        [Xunit.TraitAttribute("Description", "Registrando un donante satisfactoriamente")]
        public virtual void RegistrandoUnDonanteSatisfactoriamente()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Registrando un donante satisfactoriamente", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 5
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 6
 testRunner.Given("Soy un usuario no registrado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "field",
                            "value"});
                table1.AddRow(new string[] {
                            "firstname",
                            "Laura"});
                table1.AddRow(new string[] {
                            "lastname",
                            "Garcia"});
                table1.AddRow(new string[] {
                            "sex",
                            "F"});
                table1.AddRow(new string[] {
                            "birthdate",
                            "1998-05-20"});
                table1.AddRow(new string[] {
                            "email",
                            "l.garcia@gmail.com"});
                table1.AddRow(new string[] {
                            "phone",
                            "8094820985"});
                table1.AddRow(new string[] {
                            "username",
                            "l.garcia"});
                table1.AddRow(new string[] {
                            "password",
                            "Hola123*"});
                table1.AddRow(new string[] {
                            "confirmpassword",
                            "Hola123*"});
                table1.AddRow(new string[] {
                            "bloodtype",
                            "O+"});
                table1.AddRow(new string[] {
                            "weight",
                            "80"});
#line 7
 testRunner.When("Voy a la pagina de registro de donante e ingreso los siguientes datos", ((string)(null)), table1, "When ");
#line hidden
#line 20
 testRunner.And("Hago clic en registrarme", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 21
 testRunner.Then("Soy registrado exitosamente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 22
 testRunner.And("Recibo un correo de confirmacion con un codigo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 23
 testRunner.And("El sistema me muestra la pagina de confirmacion de correo que contiene el campo \"" +
                        "Codigo de confirmacion\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 24
 testRunner.When("Cuando lleno el campo \"Codigo de confirmacion\" con el condigo recibido <confirmat" +
                        "ionCode>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 25
 testRunner.And("Hago clic en el boton \"Enviar\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 26
 testRunner.Then("El sistema confirma mi correo", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 27
 testRunner.And("Me muestra el mensaje \"Correo electronico confirmado exitosamente\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 28
 testRunner.And("El sistema me lleva a la pagina de Preguntas de Validacion de Donante", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Registrando un usuario con campos vacios")]
        [Xunit.TraitAttribute("FeatureTitle", "Registro de donante")]
        [Xunit.TraitAttribute("Description", "Registrando un usuario con campos vacios")]
        public virtual void RegistrandoUnUsuarioConCamposVacios()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Registrando un usuario con campos vacios", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 31
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 32
 testRunner.Given("Soy un usuario no registrado", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "field",
                            "value"});
                table2.AddRow(new string[] {
                            "firstname",
                            "Jorge"});
                table2.AddRow(new string[] {
                            "lastname",
                            "Perez"});
                table2.AddRow(new string[] {
                            "sex",
                            "M"});
                table2.AddRow(new string[] {
                            "birthdate",
                            "1990-05-20"});
                table2.AddRow(new string[] {
                            "email",
                            ""});
                table2.AddRow(new string[] {
                            "phone",
                            "8094820754"});
                table2.AddRow(new string[] {
                            "username",
                            "l.garcia"});
                table2.AddRow(new string[] {
                            "password",
                            "Hola123*"});
                table2.AddRow(new string[] {
                            "confirmpassword",
                            "Hola123*"});
                table2.AddRow(new string[] {
                            "bloodtype",
                            "O+"});
                table2.AddRow(new string[] {
                            "weight",
                            "80"});
#line 33
 testRunner.When("Voy a la pagina de registro de donante e ingreso los siguientes datos", ((string)(null)), table2, "When ");
#line hidden
#line 46
 testRunner.Then("Soy registrado exitosamente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 47
 testRunner.And("Recibo un correo de confirmacion", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                RegistroDeDonanteFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                RegistroDeDonanteFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
