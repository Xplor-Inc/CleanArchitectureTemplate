import { Gender, UserRoles } from "../../../Enums";
import { IAuditableDto } from "../AuditableDto";

export interface IUserDto extends IAuditableDto {
    emailAddress?: string;
    firstName?: string;
    isActive?: boolean
    imagePath? : string
    lastLoginDate?: Date | null;
    lastName?: string
    role?:UserRoles
    gender?:Gender
}