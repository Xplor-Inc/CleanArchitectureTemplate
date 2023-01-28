import { EnquiryTypes } from "../../../Enums";
import { IAuditableDto } from "../AuditableDto";
import { IUserDto } from "../Users";

export interface IEnquiryDto extends IAuditableDto {
    email: string;
    name: string;
    message: string;
    resolution: string;
    isResolved: boolean;
    updatedBy: IUserDto;
    enquiryType:EnquiryTypes
}