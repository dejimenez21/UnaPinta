using System;
using TechTalk.SpecFlow;

namespace UnaPinta.Tests.steps
{
    [Binding]
    public class RegisterSteps
    {
        [Given(@"Soy un usuario no registrado")]
        public void GivenSoyUnUsuarioNoRegistrado()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Voy a la pagina de registro de donante e ingreso los siguientes datos")]
        public void WhenVoyALaPaginaDeRegistroDeDonanteEIngresoLosSiguientesDatos(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"Hago clic en registrarme")]
        public void WhenHagoClicEnRegistrarme()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Soy registrado exitosamente")]
        public void ThenSoyRegistradoExitosamente()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"Recibo un correo de confirmacion")]
        public void ThenReciboUnCorreoDeConfirmacion()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
