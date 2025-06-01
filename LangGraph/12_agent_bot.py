from typing import TypedDict
from langgraph.graph import StateGraph, START, END
from langchain_core.messages import HumanMessage
from langchain_openai import ChatOpenAI
from dotenv import load_dotenv

load_dotenv()

class AgentState(TypedDict):
    messages: list[HumanMessage]

llm = ChatOpenAI(model='gpt-4o')

def processor(state:AgentState) -> AgentState:    
    result = llm.invoke(state['messages'])
    print(f"\nAI: {result.content}")
    return state

graph = StateGraph(AgentState)
graph.add_node('process_node', processor)
graph.add_edge(START, 'process_node')
graph.add_edge('process_node', END)

agent = graph.compile()

inp = input("User:")
while inp != "exit":
    agentinp = AgentState(messages=[HumanMessage(content=inp)])
    agent.invoke(agentinp)
    inp = input("User:")