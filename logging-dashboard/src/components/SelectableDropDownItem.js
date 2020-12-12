import React, { useState } from 'react';
import {
    Button,DropdownItem,Tooltip
} from 'reactstrap';

const SelectableDropDownItem = (props) => {
    const [selected, setSelected] = useState(false);
    const [tooltipOpen, setTooltipOpen] = useState(false);
    const dropDownId = props.DisplayName + 'dropdownitem';
    const toggle = () => setTooltipOpen(!tooltipOpen);

    return (
        <>
            <DropdownItem id={dropDownId}>
                {selected
                    ? <Button color="dark" onClick={()=>setSelected(false)}>{props.DisplayName}</Button>
                    : <Button outline color="dark" onClick={()=>setSelected(true)}>{props.DisplayName}</Button>}
            </DropdownItem>
            <Tooltip placement="right" isOpen={tooltipOpen} target={dropDownId} toggle={toggle}>
                {props.Description}
            </Tooltip>
        </>
    );
}

export default SelectableDropDownItem;