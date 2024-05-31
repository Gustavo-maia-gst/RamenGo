using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RamenGo.Api.DTOs;
using RamenGo.Api.Middlewares;
using RamenGo.Application.Services;
using RamenGo.Domain.Entities;

namespace RamenGo.Api.Controllers
{
    [ApiKeyAuthorize]
    [Route("/api/v1/")]
    public class RamenGoController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly BrothService _brothService;
        private readonly ProteinService _proteinService;
        private readonly IMapper _mapper;

        public RamenGoController(OrderService orderService,
                                 BrothService brothService,
                                 ProteinService proteinService,
                                 IMapper mapper)
        {
            _orderService = orderService;
            _brothService = brothService;
            _proteinService = proteinService;
            _mapper = mapper;
        }

        /// <summary>
        /// List all available broths
        /// </summary>
        /// <response code="200">A list of broths</response>
        [HttpGet("broths/")]
        [ProducesResponseType(typeof(List<OrderItemDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        public IActionResult GetBroths()
        {
            List<Broth> broths = _brothService.GetBroths();
            return Ok(_mapper.Map<List<Broth>, List<OrderItemDto>>(broths));
        }

        /// <summary>
        /// List all available proteins
        /// </summary>
        /// <response code="200">A list of proteins</response>
        [HttpGet("proteins/")]
        [ProducesResponseType(typeof(List<OrderItemDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        public IActionResult GetProteins()
        {
            List<Protein> proteins = _proteinService.GetProteins();
            return Ok(_mapper.Map<List<Protein>, List<OrderItemDto>>(proteins));
        }

        /// <summary>
        /// Place an order
        /// </summary>
        /// <response code="200">Order placed successfuly</response>
        [HttpPost("orders/")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(500)]
        public IActionResult CreateOrder([FromBody] OrderCreateDto? orderCreate)
        {
            try
            {
                if (orderCreate == null || orderCreate.ProteinId == 0 || orderCreate.BrothId == 0)
                    return BadRequest(new ErrorResponse("both brothId and proteinId are required"));
                
                Order order = _orderService.CreateOrder(orderCreate.ProteinId, orderCreate.BrothId);
                return Ok(_mapper.Map<Order, OrderDto>(order));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse("brothId or proteinId not found"));
            }
        }

        /// <summary>
        /// Same as /orders, the front-end is calling /order, but was requested /orders. So there are the two endpoints
        /// </summary>
        /// <response code="200">Order placed successfuly</response>
        [HttpPost("order/")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(500)]
        public IActionResult CreateOrderFrontEnd([FromBody] OrderCreateDto? orderCreate)
        {
            try
            {
                if (orderCreate == null || orderCreate.ProteinId == 0 || orderCreate.BrothId == 0)
                    return BadRequest(new ErrorResponse("both brothId and proteinId are required"));

                Order order = _orderService.CreateOrder(orderCreate.ProteinId, orderCreate.BrothId);
                return Ok(_mapper.Map<Order, OrderDto>(order));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse("brothId or proteinId not found"));
            }
        }


        /// <summary>
        /// Get an existing order
        /// </summary>
        /// <response code="200">Order returned</response>
        /// <response code="404">Order not found</response>
        [HttpGet("orders/{id}")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public IActionResult GetOrder(int id)
        {
            try
            {
                Order order = _orderService.GetOrder(id);
                return Ok(_mapper.Map<Order, OrderDto>(order));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponse("Order not found"));
            }
        }

        /* Endpoints de criação e remoção de caldos e proteínas,
         * foram comentados porque não devem estar acessíveis para o usuário.

        /// <summary>
        /// Create a new Broth
        /// </summary>
        [HttpPost("broths/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        [ProducesResponseType(500)]
        public IActionResult CreateBroth([FromBody] OrderItemDto brothCreate)
        {
            if (brothCreate == null)
                return BadRequest();

            _brothService.CreateBroth(_mapper.Map<OrderItemDto, Broth>(brothCreate));
            return Ok();
        }

        /// <summary>
        /// Create a new Protein
        /// </summary>
        [HttpPost("proteins/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]
        [ProducesResponseType(500)]
        public IActionResult CreateProtein([FromBody] OrderItemDto? proteinCreate)
        {
            if (proteinCreate == null)
                return BadRequest();

            _proteinService.CreateProtein(_mapper.Map<OrderItemDto, Protein>(proteinCreate));
            return Ok();
        }
        */
    }
}
