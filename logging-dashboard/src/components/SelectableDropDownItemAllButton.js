import React, { useState } from 'react';
import { Button, Tooltip, DropdownItem } from 'reactstrap';

const SelectableDropDownItemAllButton = (props) => {
    const [selected, setSelected] = useState(props.Selected);
    const [tooltipOpen, setTooltipOpen] = useState(false);
    const dropDownId = props.id + 'dropdownallbutton';
    const toggle = () => setTooltipOpen(!tooltipOpen);

    return (
        <>
           <DropdownItem id={dropDownId}>
                {selected
                    ? <Button id={dropDownId} color="dark" onClick={() => setSelected(false)}>{props.DisplayName}</Button>
                    : <Button id={dropDownId} outline color="dark" onClick={() => setSelected(true)}>{props.DisplayName}</Button>}
            </DropdownItem>
            <Tooltip placement="right" isOpen={tooltipOpen} target={dropDownId} toggle={toggle}>
                {props.Description}
            </Tooltip>
        </>
    );
}

export default SelectableDropDownItemAllButton;