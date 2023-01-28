import DropdownList from 'react-widgets/cjs/DropdownList';
import { IAutoCompleteDto } from '../../Components/Core/Dto/AutoComppletes';

export const AutoCompleteDropdown = ({ addDefaultOption, data, defaultValue, onChange, disabled }:
    {
        addDefaultOption?: string
        data: IAutoCompleteDto[],
        defaultValue?: IAutoCompleteDto,
        disabled?: boolean,
        onChange: (selected: IAutoCompleteDto) => void
    }) => {
    if (data.find(e => e.id === 0))
        data.shift();
    if (addDefaultOption) {
        if (!data.find(e => e.id === 0))
            data.unshift({ name: addDefaultOption, id: 0 });
    }
    return <DropdownList
        value={defaultValue}
        data={data}
        dataKey='id'
        textField='name'
        filter='startsWith'
        onChange={onChange}
        disabled={disabled}
    />
}