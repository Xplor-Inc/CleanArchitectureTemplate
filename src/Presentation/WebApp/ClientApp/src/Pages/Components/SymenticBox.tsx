import React, { useContext } from "react";
import { Accordion, Card } from "react-bootstrap";
import { AccordionContext, useAccordionButton } from "react-bootstrap";
import { FaChevronDown, FaChevronUp } from "react-icons/fa";

interface IBoxProp {
    children: React.ReactNode,
    title?: string
    key?: string
}

export const SymenticBox = ({ title, children, key }: IBoxProp) => {
    if (!key) key = "0";
    return (
        <Accordion defaultActiveKey={key}>
            {
                title &&
                <ContextAwareToggle eventKey={key}>
                    {title}
                </ContextAwareToggle>
            }
            <Accordion.Collapse eventKey={key}>
                <Card.Body className={"w-100"}>
                    {children}
                </Card.Body>
            </Accordion.Collapse>
        </Accordion>
        // <div className="card">
        //     <h3 className="card-header">
        //         {title}
        //     </h3>
        //     <Card.Body className="w-100">
        //         {children}
        //     </Card.Body>
        // </div>
    )
}



interface IProp {
    children?: any,
    eventKey: string,
    callback?: any
}

export const ContextAwareToggle = ({ children, eventKey, callback }: IProp) => {
    const { activeEventKey } = useContext(AccordionContext);

    const decoratedOnClick = useAccordionButton(
        eventKey,
        () => callback && callback(eventKey),
    );

    return (
        <h3 className="card-header"
            role="button"
            onClick={decoratedOnClick}>
            {children}
            <span>
                {activeEventKey !== eventKey && <FaChevronUp />}
                {activeEventKey === eventKey && <FaChevronDown />}
            </span>
        </h3>
    );
}