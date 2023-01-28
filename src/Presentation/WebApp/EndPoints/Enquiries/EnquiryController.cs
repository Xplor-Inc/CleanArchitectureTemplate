﻿using CleanArchitectureTemplate.Core.Interfaces.Utility;

namespace CleanArchitectureTemplate.WebApp.EndPoints.Enquiries;

[Route("api/1.0/enquiries")]
public class EnquiryController : CleanArchitectureTemplateController
{
    private IRepositoryConductor<Enquiry>    EnquiryConductor   { get; }
    private IMapper                          Mapper             { get; }
    public IUserAgentConductor               UserAgentConductor { get; }

    public EnquiryController(
        IRepositoryConductor<Enquiry>   enquiryConductor,
        IMapper                         mapper,
        IUserAgentConductor             userAgentConductor)
    {
        EnquiryConductor    = enquiryConductor;
        Mapper              = mapper;
        UserAgentConductor  = userAgentConductor;
    }



    [AppAuthorize(UserRole.Admin)]
    [HttpGet]
    public IActionResult Index(
        string?     searchText,
        string      sortBy      = "CreatedOn",
        string      sortOrder   = "DESC",
        int         skip        = 0,
        int         take        = 5)
    {
        Expression<Func<Enquiry, bool>> predicate = e => true;

        if (!string.IsNullOrEmpty(searchText))
        {
            predicate = predicate.AndAlso(e => e.Name.Contains(searchText) 
                                            || e.Email.Contains(searchText)
                                            || e.Message.Contains(searchText));
        }

        var enquiryResult = EnquiryConductor.FindAll(filter: predicate, e => e.OrderBy(sortBy, sortOrder), skip: skip, take: take);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiries = enquiryResult.ResultObject.ToList();
        var rowCount = EnquiryConductor.FindAll(filter: predicate).ResultObject.Select(e => e.Id).Count();
        var dtos = Mapper.Map<List<EnquiryDto>>(enquiries);
        return Ok(dtos, rowCount);
    }


    [HttpPost]
    public IActionResult Post([FromBody] EnquiryDto dto)
    {       
        var enquiry = Mapper.Map<Enquiry>(dto);
        var visitorId = GetVisitorId();
        enquiry.VisitorId = visitorId;
        var createResult = EnquiryConductor.Create(enquiry, CurrentUserId);
        if (createResult.HasErrors)
        {
            return InternalError<EnquiryDto>(createResult.Errors);
        }

        SaveCount(visitorId, false);


        return Ok(true);
    }


    [AppAuthorize(UserRole.Admin)]
    [HttpPut("{id:Guid}")]
    public IActionResult Put(Guid id, [FromBody] EnquiryResolutionDto dto)
    {
        var enquiryResult = EnquiryConductor.FindAll(e=>e.UniqueId == id);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiry = enquiryResult.ResultObject.FirstOrDefault();
        if (enquiry == null)
        {
            return InternalError<EnquiryDto>("Invalid enquiry");
        }

        enquiry.IsResolved   = true;
        enquiry.Resolution   = dto.Resolution;

        var updateResult = EnquiryConductor.Update(enquiry, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<EnquiryDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }


    [AppAuthorize(UserRole.Admin)]
    [HttpDelete("{id:Guid}")]
    public IActionResult Delete(Guid id)
    {
        var enquiryResult = EnquiryConductor.FindAll(e => e.UniqueId == id);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiry = enquiryResult.ResultObject.FirstOrDefault();
        if (enquiry == null)
        {
            return InternalError<EnquiryDto>("Invalid enquiry");
        }

        var updateResult = EnquiryConductor.Delete(enquiry, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<EnquiryDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }
}