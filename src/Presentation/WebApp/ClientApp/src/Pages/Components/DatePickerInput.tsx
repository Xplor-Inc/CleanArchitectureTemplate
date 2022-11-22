import React, { memo } from "react";
import ReactDatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { Utility } from "../../Components/Service/Utility";

interface IDatePickerProps {
    onChange: (date:Date | null | undefined) => void
    maxDate?: Date | null
    minDate?: Date | null
    placeholder?: string
    value?: Date | null
}

const DatePickerInput = (props: IDatePickerProps) => {
    const { onChange, maxDate, minDate, placeholder, value } = props;
    return (
        <ReactDatePicker placeholderText={placeholder}
            onChange={(date) => { onChange(date) }}
            value={Utility.Format.Date_DD_MMM_YYYY(value)}
            selected={value}
            maxDate={maxDate}
            minDate={minDate}
            isClearable
            customInput={<CustomDatePickerInput />} />
    )
}

const CustomDatePickerInput = React.forwardRef<HTMLInputElement, React.HTMLProps<HTMLInputElement>>((props, ref) => {
    return <input type="text" className="form-control"
        onClick={props.onClick}
        ref={ref}
        onChange={props.onChange}
        value={props.value}
        readOnly
        placeholder={props.placeholder}
        style={{ backgroundColor: "white" }} />
});

export default memo(DatePickerInput);
