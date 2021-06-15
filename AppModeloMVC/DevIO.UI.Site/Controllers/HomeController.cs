using DevIO.UI.Site.Data;
using DevIO.UI.Site.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DevIO.UI.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly OperacaoService _operacaoService;
        private readonly OperacaoService _operacaoService2;

        public HomeController(IPedidoRepository pedidoRepository, OperacaoService operacaoService, OperacaoService operacaoService2)
        {
            _pedidoRepository = pedidoRepository;
            _operacaoService = operacaoService;
            _operacaoService2 = operacaoService2;
        }

        public IActionResult Index()
        {
            var pedido = _pedidoRepository.ObterPedido();

            return View();
        }

        public IActionResult Index1([FromServices] IPedidoRepository _pedidoRepository2)
        {
            var pedido = _pedidoRepository2.ObterPedido();

            return View();
        }

        public string Index2()
        {
            return RetornaOperacao();
        }

        private string RetornaOperacao()
        {
            return
                "Primeira instância: " + Environment.NewLine +
                _operacaoService.Transient.OperacaoId + Environment.NewLine +
                _operacaoService.Scoped.OperacaoId + Environment.NewLine +
                _operacaoService.Singleton.OperacaoId + Environment.NewLine +
                _operacaoService.SingletonInstance.OperacaoId + Environment.NewLine +

                Environment.NewLine +
                Environment.NewLine +

                "Segunda instância: " + Environment.NewLine +
                _operacaoService2.Transient.OperacaoId + Environment.NewLine +
                _operacaoService2.Scoped.OperacaoId + Environment.NewLine +
                _operacaoService2.Singleton.OperacaoId + Environment.NewLine +
                _operacaoService2.SingletonInstance.OperacaoId + Environment.NewLine;
        }

        private string RetornaOperacaoFormatada()
        {
            return "<b>Primeira instância</b>" + Environment.NewLine + "<hr />" + Environment.NewLine +
                "<b>Transient:</b> " + _operacaoService.Transient.OperacaoId + Environment.NewLine +
                "<b>Scoped:</b> " + _operacaoService.Scoped.OperacaoId + Environment.NewLine +
                "<b>Singleton:</b> " + _operacaoService.Singleton.OperacaoId + Environment.NewLine +
                "<b>SingletonInstance:</b> " + _operacaoService.SingletonInstance.OperacaoId + Environment.NewLine +

                Environment.NewLine +
                Environment.NewLine +

                "<b>Segunda instância</b>" + Environment.NewLine + "<hr />" + Environment.NewLine +
                "<b>Transient:</b> " + _operacaoService2.Transient.OperacaoId + Environment.NewLine +
                "<b>Scoped:</b> " + _operacaoService2.Scoped.OperacaoId + Environment.NewLine +
                "<b>Singleton:</b> " + _operacaoService2.Singleton.OperacaoId + Environment.NewLine +
                "<b>SingletonInstance:</b> " + _operacaoService2.SingletonInstance.OperacaoId + Environment.NewLine;
        }
    }

}
